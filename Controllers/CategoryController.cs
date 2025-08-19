using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Models;
using ReservationSystem.Repositories.Interfaces;

[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryController(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<IActionResult> ManageCategories()
    {
        var categories = await _categoryRepo.GetAllAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult AddCategory()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveAddCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveAsync();
            return RedirectToAction("ManageCategories");
        }

        return View("AddCategory", category);
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveEditCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            await _categoryRepo.UpdateAsync(category);
            await _categoryRepo.SaveAsync();
            return RedirectToAction("ManageCategories");
        }

        return View("EditCategory", category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryRepo.DeleteAsync(id);
        await _categoryRepo.SaveAsync();
        return RedirectToAction("ManageCategories");
    }
}
