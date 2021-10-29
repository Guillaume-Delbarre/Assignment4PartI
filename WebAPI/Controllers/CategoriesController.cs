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
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();

            return Ok(categories.Select(x => GetCategoryUrlViewModel(x)));
        }

        // api/categories/id
        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel model = GetCategoryViewModel(category);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryViewModel model)
        {

            var category = _dataService.CreateCategory(model.Name, model.Description);

            return Created(GetUrl(category), GetCategoryViewModel(category));

        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryViewModel model)
        {
            if (id != model.Id)
                return NotFound();
            var category = _dataService.GetCategory(id);
            if (_dataService.UpdateCategory(id, model.Name, model.Description))
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (_dataService.DeleteCategory(id))
                return Ok(GetCategoryViewModel(category));
            else
                return NotFound();
        }

        private string GetUrl(Category category)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
        }
        private CategoryUrlViewModel GetCategoryUrlViewModel(Category category)
        {
            return new CategoryUrlViewModel
            {
                Url = GetUrl(category),
                Name = category.Name,
                Desc = category.Description
            };
        }

        private CategoryViewModel GetCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
