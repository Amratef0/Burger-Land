using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Models;
using ReservationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepo;
    private readonly ApplicationDbContext _context;

    public ProductController(IProductRepository productRepo, ApplicationDbContext context)
    {
        _productRepo = productRepo;
        _context = context;
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveAddProduct(Product product)
    {
        if (ModelState.IsValid)
        {
            if (!string.IsNullOrEmpty(product.ImagePath))
                product.ImagePath = "/Images/" + product.ImagePath;

            await _productRepo.AddAsync(product);
            await _productRepo.SaveAsync();
            return RedirectToAction("ManageProduct");
        }

        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View("AddProduct", product);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null) return NotFound();

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveEditProduct(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productRepo.UpdateAsync(product);
            await _productRepo.SaveAsync();
            return RedirectToAction("ManageProduct");
        }

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
        return View("EditProduct", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productRepo.DeleteAsync(id);
        await _productRepo.SaveAsync();
        return RedirectToAction("ManageProduct");
    }

    public async Task<IActionResult> ManageProduct()
    {
        var products = await _productRepo.GetAllAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await _productRepo.GetByCategoryAsync(categoryId);
        return Json(products);
    }
}
