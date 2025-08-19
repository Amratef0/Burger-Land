using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using ReservationSystem.Repositories.Implementations;
using ReservationSystem.Repositories.Interfaces;
[Authorize]
public class MainController : Controller
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IReservationRepository _reservationRepo;
    public MainController(IProductRepository productRepo, ICategoryRepository categoryRepo, IReservationRepository reservationRepo)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _reservationRepo = reservationRepo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var viewModel = new MainViewModel
        {
            Products = (await _productRepo.GetAllAsync())
                        .OrderByDescending(p => p.DateAdded)
                        .Take(8)
                        .ToList(),
            Reservation = new Reservation(),
            Categories = (await _categoryRepo.GetAllAsync()).ToList()
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult CreateReservation()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveCreateReservation(Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            await _reservationRepo.AddAsync(reservation);
            await _reservationRepo.SaveAsync();
            return RedirectToAction(nameof(Confirmation));
        }
        return View(reservation);
    }

    public IActionResult Confirmation()
    {
        return View();
    }
    [AllowAnonymous]
    public async Task<IActionResult> LoadMoreProducts(int page = 1, int categoryId = 0)
    {
        int pageSize = 8; 

        var products = await _productRepo.GetAllAsync();

        if (categoryId != 0)
        {
            products = products.Where(p => p.CategoryId == categoryId);
        }

        var paginatedProducts = products
            .OrderByDescending(p => p.DateAdded)
            .Take(pageSize) 
            .ToList();

        return PartialView("_ProductList", paginatedProducts);
    }


    public IActionResult About()
    {
        return View();
    }
    [AllowAnonymous]
    public async Task<IActionResult> OrderNow(int page = 1, int categoryId = 0)
    {
        int pageSize = 9;

        var products = await _productRepo.GetAllAsync();

        if (categoryId != 0)
            products = products.Where(p => p.CategoryId == categoryId);

        var paginatedProducts = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ProductList", paginatedProducts);

        var model = new MainViewModel
        {
            Categories = (await _categoryRepo.GetAllAsync()).ToList(),
            Products = paginatedProducts
        };

        return View(model);
    }



}
