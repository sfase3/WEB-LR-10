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
    public class ShopCardController : ControllerBase
    {
        private readonly IShopCartService _ShopCardService;

        public ShopCardController(IShopCartService ShopCardService)
        {
            _ShopCardService = ShopCardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopCard>>> Get()
        {
            var ShopCard = await _ShopCardService.GetAllShopCards();
            return Ok(ShopCard);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShopCard>> Get(int id)
        {
            var ShopCard = await _ShopCardService.GetShopCard(id);
            if (ShopCard.Data == null)
            {
                return NotFound();
            }
            return Ok(ShopCard.Data);
        }

        [HttpPost]
        public async Task<ActionResult<ShopCard>> Post(ShopCard ShopCard)
        {
            var newShopCard = await _ShopCardService.AddShopCard(ShopCard);
            return CreatedAtAction(nameof(Get), new { id = newShopCard.Data.ShopCardId }, newShopCard.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ShopCard ShopCard)
        {
            if (id != ShopCard.ShopCardId)
            {
                return BadRequest();
            }

            var updatedShopCard = await _ShopCardService.UpdateShopCard(id, ShopCard);
            if (updatedShopCard.Data == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedShopCard = await _ShopCardService.DeleteShopCard(id);
            if (deletedShopCard.Data == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
