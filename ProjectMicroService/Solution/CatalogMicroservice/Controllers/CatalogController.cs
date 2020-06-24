using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogMicroservice.Model;
using CatalogMicroservice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CatalogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogController(IMongoDatabase db)
        {
            _catalogRepository = new CatalogRepository(db);
        }
        // GET api/<CatalogController>/110ec627-2f05-4a7e-9a95-7a91e8005da8
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var catalogItem = _catalogRepository.GetCatalogItem(id);
            return new OkObjectResult(catalogItem);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var catalogItems = _catalogRepository.GetCatalogItems();
            return new OkObjectResult(catalogItems);
        }
        [HttpPost]
        public IActionResult Post([FromBody]CatalogItem catalogItem)
        {
            _catalogRepository.InsertCatalogItem(catalogItem);
            return CreatedAtAction(nameof(Get), new { id = catalogItem.Id }, catalogItem);
        }
    }
}
