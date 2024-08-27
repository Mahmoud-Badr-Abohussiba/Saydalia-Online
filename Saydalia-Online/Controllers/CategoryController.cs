using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Helpers;
using Saydalia_Online.Models;
using Saydalia_Online.ViewModels;

namespace Saydalia_Online.Controllers
{
    public class CategoryController : Controller
    {
        SaydaliaOnlineContext _dbContext = new SaydaliaOnlineContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.categories.Add(model);
                    _dbContext.SaveChanges();
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
            var category = _dbContext.categories.FirstOrDefault(c => c.Id == id);
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
                    var category = _dbContext.categories.FirstOrDefault(c => c.Id == id);

                    category.UpdatedAt = DateTime.Now;
                    category.Name = model.Name;
                    //_dbContext.categories.Update(model);
                    await _dbContext.SaveChangesAsync();
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
