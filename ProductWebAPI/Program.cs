using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductWebAPI.Models;
using ProductWebAPI.Services;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IShopCartService, ShopCartService>();
builder.Services.AddSingleton<ICatalogService, CatalogService>();
builder.Services.AddSingleton<ICatalogService, CatalogService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddTransient<IVersionService, VersionService>();
builder.Services.AddSingleton<IHealthCheck, HealthCheck>();

builder.Services.AddHealthChecks()
    .AddCheck<HealthCheck2>("myhealth_check")
    .AddCheck<HealthCheck2>("myservice1_health_check");

builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "sql-server-check");

builder.Services.AddDbContext<HealthCheckDB>(options =>
{
    options.UseSqlServer("Server=DESKTOP-HHN2ULA\\SQLEXPRESS;;Database=ProductWebAPI;Trusted_Connection=True;TrustServerCertificate=True;");
}
);

var app = builder.Build();

app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c =>
    {
        c.OAuthClientId("swagger");
        c.OAuthAppName("Your ATB - Swagger");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Map("/login/{username}", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    return new JwtSecurityTokenHandler().WriteToken(jwt);
});

app.UseHealthChecks("/healthcheck1", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("my_service1_health_check"),
});

app.UseHealthChecks("/health2");

app.UseHealthChecks("/sqlserverhealth", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("sql_server_health_check"),
});

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui";
});

app.Run();
