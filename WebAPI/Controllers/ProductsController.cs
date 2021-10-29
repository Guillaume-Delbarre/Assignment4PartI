using Assignment4PartI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using WebAPI.ViewModels;
using Assignment4PartI.Domain;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductIdController : Controller
    {
        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public ProductIdController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }


    [ApiController]
    [Route("api/products/category")]
    public class ProductIdCategoryController : Controller
    {
        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public ProductIdCategoryController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{id}")]
        public IActionResult GetArrayProductsByCategory(int id)
        {
            var products = _dataService.GetProductByCategory(id);
            if (products.Count == 0)
                return NotFound(products);
            return Ok(products);
        }
    }


    [ApiController]
    [Route("api/products/name")]
    public class ProductIdSubStringController : Controller
    {
        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public ProductIdSubStringController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{substring}")]
        public IActionResult GetArrayProductsByName(string substring)
        {
            var products = _dataService.GetProductByName(substring);
            if (products.Count == 0)
                return NotFound(products);
            return Ok(products);
        }
    }
}
