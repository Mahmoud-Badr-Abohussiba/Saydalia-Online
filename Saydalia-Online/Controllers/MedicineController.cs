using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Saydalia_Online.Helpers;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MedicineController(
            IMedicineRepository medicineRepository ,
            ICategoryRepository categoryRepository
            )
        {
            _medicineRepository=medicineRepository;
            _categoryRepository=categoryRepository;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var catgs = await _categoryRepository.GetAll();
            ViewBag.MedicineCategories = catgs;

            await next(); // This ensures the action method executes after your logic
        }

        public async Task<IActionResult> Index(int? page)
        {
            var medicines = await _medicineRepository.GetAllPaginated(page??1);
            ViewBag.Medicines = medicines;
            return View(medicines);
        }
        public async Task<IActionResult> Details(int id) 
        {
            var medicine = await _medicineRepository.Details(id);
            return View(medicine);
        }

        [HttpGet]
        [Authorize(Roles = "Pharmacist, Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAll();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Pharmacist, Admin")]
        public async Task<IActionResult> Create(Medicine medicin ,  IFormFile image)
        {
            ViewBag.Categories = await _categoryRepository.GetAll();
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    medicin.ImageName = await DocumentSettings.UploadFile(image, "images");
                }

                await _medicineRepository.Add(medicin);
                return RedirectToAction(nameof(Index));
            }
            return View(medicin);
        }

        [HttpGet]
        [Authorize(Roles = "Pharmacist, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var medicine = await _medicineRepository.GetById(id.Value);
            if (medicine == null)
            {
                return BadRequest();
            }
            ViewBag.Categories = await _categoryRepository.GetAll();
            return View(medicine);
        }

        [HttpPost]
        [Authorize(Roles = "Pharmacist, Admin")]
        public async Task<IActionResult> Edit([FromRoute] int id, Medicine model, IFormFile? Image)
        {
            ViewBag.Categories = await _categoryRepository.GetAll();

            if (id != model.Id)
                return BadRequest();



            if (ModelState.IsValid)
            {
                try
                {

                    var oldMedicine = await _medicineRepository.GetByIdAsNoTracking(id);

                    if (oldMedicine == null)
                        return BadRequest();

                    if (oldMedicine.ImageName != null && Image != null)
                    {
                        string oldImageName = oldMedicine.ImageName;
                        DocumentSettings.DeleteFile(oldImageName, "images");
                    }

                    if(Image != null)
                    {
                        model.ImageName = await DocumentSettings.UploadFile(Image, "images");
                    }
                    model.UpdatedAt = DateTime.Now;
                
                    var result = await _medicineRepository.Update(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DisplayUsingNameFromAToZ()
        {
            var medicines = await _medicineRepository.DisplayUsingNameFromAToZ();
            return View(nameof(Index) ,medicines);
        }
        public async Task<IActionResult> DisplayUsingNameFromZToA()
        {
            var medicines = await _medicineRepository.DisplayUsingNameFromZToA();
            return View(medicines);
        }
        public async Task<IActionResult> DisplayUsingPriceLowToHigh()
        {
            var medicines = await _medicineRepository.DisplayUsingPriceLowToHigh();
            return View(medicines);
        }
        public async Task<IActionResult> DisplayUsingPriceHighToLow()
        {
            var medicines = await _medicineRepository.DisplayUsingPriceHighToLow();
            return View(medicines);
        }

        public async Task<IActionResult> Search(string search)
        {
            var medicines = await _medicineRepository.SearchByName(search);
            ViewBag.Medicines = medicines;
            return View("Index", medicines);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var medicne = await _medicineRepository.Details(id);
            if (medicne.OrderItems.IsNullOrEmpty())
            {
                 await _medicineRepository.Delete(medicne);
                 return RedirectToAction("index");
            }

            TempData["errorMessage"] = "Can not delete medicine used in orders";

            return RedirectToAction("index");

        }
    }
}
