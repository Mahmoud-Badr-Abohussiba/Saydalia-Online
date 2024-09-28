using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Helpers;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;
using System.Reflection.Metadata.Ecma335;

namespace Saydalia_Online.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //OnActionExecuting function is being called when any action in it's containing controller called
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var catgs = await _categoryRepository.GetAll();
            //var medicineCategories = GetMedicineCategories();

            ViewBag.MedicineCategories = catgs;

            base.OnActionExecuting(filterContext);
        }
        public async Task<IActionResult> Index()
        {

			var categoryid = await _categoryRepository.GetAll();

			return View(categoryid);
		}

		public async Task<IActionResult> Details(int id)
        {
            var cat = await _categoryRepository.GetByIdWithProducts(id);
                                
            return View(cat);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.Add(model);
                    return RedirectToAction(nameof(Index));
                }catch(Exception error)
                {
                    ModelState.AddModelError(string.Empty, error.Message);
                }
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int ? id)
        {
            var category = _categoryRepository.GetById(id.Value);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int ? id, Category model)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _categoryRepository.GetById(id.Value);

                    category.UpdatedAt = DateTime.Now;
                    category.Name = model.Name;
                    //_dbContext.categories.Update(model);
                    await _categoryRepository.Update(category);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }
    }
}
