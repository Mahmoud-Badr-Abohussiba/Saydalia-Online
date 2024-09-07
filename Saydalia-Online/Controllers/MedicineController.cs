using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Helpers;
using Saydalia_Online.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Controllers
{
    public class MedicineController : Controller
    {

        SaydaliaOnlineContext _dbContext = new SaydaliaOnlineContext();


        //OnActionExecuting function is being called when any action in it's containing controller called
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var catgs = _dbContext.categories.ToList();
            //var medicineCategories = GetMedicineCategories();

            ViewBag.MedicineCategories = catgs;

            base.OnActionExecuting(filterContext);
        }
        public IActionResult Index()
        {
            var medicines = _dbContext.Medicines.ToList();
            ViewBag.Medicines = medicines;
            return View(medicines);
        }

        public IActionResult Details(int id) 
        {
            var medicine = _dbContext.Medicines
                                     .Include(m => m.Categories)
                                      .FirstOrDefault(m => m.Id == id);
            return View(medicine);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _dbContext.categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Medicine medicin ,  IFormFile image)
        {
            ViewBag.Categories = _dbContext.categories.ToList();
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    medicin.ImageName = await DocumentSettings.UploadFile(image, "images");
                }

                _dbContext.Medicines.Add(medicin);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(medicin);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var medicine = _dbContext.Medicines.FirstOrDefault(x => x.Id == id);
            if (medicine == null)
            {
                return BadRequest();
            }
            ViewBag.Categories = _dbContext.categories.ToList();
            return View(medicine);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, Medicine model, IFormFile Image)
        {
            ViewBag.Categories = _dbContext.categories.ToList();

            if (id != model.Id)
                return BadRequest();



            if (ModelState.IsValid)
            {
                try
                {

                    var oldMedicine = _dbContext.Medicines.Where(m => m.Id == id).AsNoTracking().FirstOrDefault();

                    if (oldMedicine == null)
                        return BadRequest();

                    if (oldMedicine.ImageName != null)
                    {
                        string oldImageName = oldMedicine.ImageName;
                        DocumentSettings.DeleteFile(oldImageName, "images");
                    }

                    model.ImageName = await DocumentSettings.UploadFile(Image, "images");
                    model.UpdatedAt = DateTime.Now;
                
                    _dbContext.Medicines.Update(model);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public IActionResult DisplayUsingNameFromAToZ()
        {
            var medicines = _dbContext.Medicines.OrderBy(m => m.Name).ToList();
            return View(nameof(Index) ,medicines);
        }
        public IActionResult DisplayUsingNameFromZToA()
        {
            var medicines = _dbContext.Medicines.OrderByDescending(m => m.Name).ToList();
            return View(medicines);
        }
        public IActionResult DisplayUsingPriceLowToHigh()
        {
            var medicines = _dbContext.Medicines.OrderBy(m => m.Price).ToList();
            return View(medicines);
        }
        public IActionResult DisplayUsingPriceHighToLow()
        {
            var medicines = _dbContext.Medicines.OrderByDescending(m => m.Price).ToList();
            return View(medicines);
        }

        public IActionResult Search(string search)
        {
            var medicines = _dbContext.Medicines.Where(m=>m.Name.Contains(search)).ToList();
            ViewBag.Medicines = medicines;
            return View("Index", medicines);
        }
    }
}
