using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Helpers;
using Saydalia_Online.Models;
using Saydalia_Online.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Controllers
{
    public class MedicineController : Controller
    {

        SaydaliaOnlineContext _dbContext = new SaydaliaOnlineContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id) 
        {
            var medicine = _dbContext.Medicines
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
        public async Task<IActionResult> Create(MedicineViewModel modelVM)
        {
            ViewBag.Categories = _dbContext.categories.ToList();

            if (ModelState.IsValid) {
                modelVM.ImageName = await DocumentSettings.UploadFile(modelVM.Image, "images");
                var NewMedicine = new Medicine() 
                {
                    Name = modelVM.Name,
                    Description = modelVM.Description,
                    ImageName = modelVM.ImageName,
                    Price = modelVM.Price,
                    Stock = modelVM.Stock,
                    Cat_Id = modelVM.Cat_Id
                };
                _dbContext.Medicines.Add(NewMedicine);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(modelVM);
        }

        [HttpGet]
        public IActionResult Edit(int ? id)
        {
            var medicine = _dbContext.Medicines.FirstOrDefault(x => x.Id == id);
            var model = new MedicineViewModel()
            {
                Name = medicine.Name,
                Description = medicine.Description,
                ImageName = medicine.ImageName,
                Price = medicine.Price,
                Stock = medicine.Stock,
                Cat_Id = medicine.Cat_Id,
                UpdatedAt = DateTime.Now
            };
            ViewBag.Categories = _dbContext.categories.ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, MedicineViewModel modelVM)
        {
            ViewBag.Categories = _dbContext.categories.ToList();
            if (id != modelVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedMedicine = new Medicine()
                    {
                        CreatedAt = modelVM.CreatedAt,
                        Name = modelVM.Name,
                        Description = modelVM.Description,
                        ImageName = modelVM.ImageName,
                        Price = modelVM.Price,
                        Stock = modelVM.Stock,
                        Cat_Id = modelVM.Cat_Id,
                        UpdatedAt = DateTime.Now,
                        Id = modelVM.Id
                    };
                    
                    var result = _dbContext.Update(UpdatedMedicine);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message); 
                }
            }
            return View(modelVM);
        }

    }
}
