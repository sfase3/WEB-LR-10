using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProductWebAPI.Models;
using ProductWebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProductWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _CatalogService;

        public CatalogController(ICatalogService CatalogService)
        {
            _CatalogService = CatalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalog>>> Get()
        {
            var Catalog = await _CatalogService.GetAllCatalogs();
            return Ok(Catalog);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Catalog>> Get(int id)
        {
            var Catalog = await _CatalogService.GetCatalog(id);
            if (Catalog == null)
            {
                return NotFound();
            }
            return Ok(Catalog.Data);
        }
        [HttpPost]
        public async Task<ActionResult<Catalog>> Post(Catalog Catalog)
        {
            var newCatalog = await _CatalogService.AddCatalog(Catalog);
            return CreatedAtAction(nameof(Get), new { id = newCatalog.Data.CatalogId }, newCatalog.Data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Catalog Catalog)
        {
            if (id != Catalog.CatalogId)
            {
                return BadRequest();
            }

            var updatedCatalog = await _CatalogService.UpdateCatalog(id,Catalog);
            if (updatedCatalog.Data == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedCatalog = await _CatalogService.DeleteCatalog(id);
            if (deletedCatalog.Data == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
