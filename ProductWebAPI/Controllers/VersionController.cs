using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Authorize]
    public class VersionController : ControllerBase
    {
        private readonly IVersionService _VersionService;

        public VersionController(IVersionService VersionService)
        {
            _VersionService = VersionService;
        }

        [HttpGet]
        public IActionResult Get(ApiVersion version)
        {
            if (version.ToString() == "1.0")
            {
                return Ok(550);
            }
            else if (version.ToString() == "2.0")
            {
                return Ok("It`s version 2.0");
            }
            else if (version.ToString() == "3.0")
            {
                var stream = new MemoryStream();
                _VersionService.GenerateExcel();
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "excel_1.xlsx");
            }
            else
            {
                return BadRequest("Error!");
            }
        }
    }
}