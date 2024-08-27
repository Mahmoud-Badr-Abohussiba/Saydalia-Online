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
            var medicines = _dbContext.Medicines.ToList();
            ViewBag.Medicines = medicines;
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
        public async Task<IActionResult> Create(MedicineViewModel medicinVM)
        {
            ViewBag.Categories = _dbContext.categories.ToList();

            if (ModelState.IsValid) {
                medicinVM.ImageName = await DocumentSettings.UploadFile(medicinVM.Image, "images");
                var NewMedicine = new Medicine() 
                {
                    Name = medicinVM.Name,
                    Description = medicinVM.Description,
                    ImageName = medicinVM.ImageName,
                    Price = medicinVM.Price,
                    Stock = medicinVM.Stock,
                    Cat_Id = medicinVM.Cat_Id
                };
                _dbContext.Medicines.Add(NewMedicine);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(medicinVM);
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

                    var oldMedicine = _dbContext.Medicines.Where(m => m.Id == id).FirstOrDefault();

                    if (oldMedicine == null)
                        return BadRequest();

                    if(oldMedicine.ImageName != null)
                    {
                        string oldImageName = oldMedicine.ImageName;
                        DocumentSettings.DeleteFile(oldImageName, "images");
                    }


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
