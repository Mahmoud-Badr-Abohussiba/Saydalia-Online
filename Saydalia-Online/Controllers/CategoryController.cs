using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Saydalia_Online.Helpers;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;
using System.Reflection.Metadata.Ecma335;

namespace Saydalia_Online.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMedicineRepository _medicineRepository;

        public CategoryController(ICategoryRepository categoryRepository, IMedicineRepository medicineRepository)
        {
            _categoryRepository = categoryRepository;
            _medicineRepository = medicineRepository;
        }

        //OnActionExecuting function is being called when any action in it's containing controller called
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var catgs = await _categoryRepository.GetAll();
            //var medicineCategories = GetMedicineCategories();

            ViewBag.MedicineCategories = catgs;

            base.OnActionExecuting(filterContext);
        }
        public async Task<IActionResult> Index(int?page)
        {

			var categoryid = await _categoryRepository.GetAllPaginated(page??1);

			return View(categoryid);
		}

		public async Task<IActionResult> Details(int id,int? page)
        {
            var cat = await _medicineRepository.GetAllForCategory(id, page??1);
                                
            return View(cat);
        }

        [HttpGet]
        [Authorize(Roles = "Pharmacist, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Pharmacist, Admin")]
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
        [Authorize(Roles = "Pharmacist, Admin")]
        public async Task<IActionResult> Edit(int ? id)
        {
            var category = await _categoryRepository.GetById(id.Value);
            return View(category);
        }

        [HttpPost]
        [Authorize(Roles = "Pharmacist, Admin")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _categoryRepository.GetByIdWithProducts(id);
            if ( !model.Medicines.IsNullOrEmpty() )
            {
                foreach(var item in model.Medicines)
                {
                    await _medicineRepository.Delete(item);
                }
            }
            await _categoryRepository.Delete(model);
            return RedirectToAction("Index");
        }


    }
}
