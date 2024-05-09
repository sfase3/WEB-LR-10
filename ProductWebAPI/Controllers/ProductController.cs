using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;
using ProductWebAPI.Services;

namespace ProductWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetProduct(id);
            if (product.Data == null)
            {
                return NotFound();
            }
            return Ok(product.Data);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            var newProduct = await _productService.AddProduct(product);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Data.Id }, newProduct.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var updatedProduct = await _productService.UpdateProduct(id, product);
            if (updatedProduct.Data == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _productService.DeleteProduct(id);
            if (deletedProduct.Data == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
