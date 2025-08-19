using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Models;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    public CartController(ApplicationDbContext context, ICartService cartService, IOrderService orderService)
    {
        _context = context;
        _cartService = cartService;
        _orderService = orderService;
    }

    public IActionResult CartIndex()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }

    public async Task<IActionResult> AddToCart(int id)
    {
        await _cartService.AddToCart(id);
        return RedirectToAction("CartIndex");
    }

    public IActionResult RemoveFromCart(int id)
    {
        _cartService.RemoveFromCart(id);
        return RedirectToAction("CartIndex");
    }

    public IActionResult IncreaseQuantity(int id)
    {
        _cartService.IncreaseQuantity(id);
        return RedirectToAction("CartIndex");
    }

    public IActionResult DecreaseQuantity(int id)
    {
        _cartService.DecreaseQuantity(id);
        return RedirectToAction("CartIndex");
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = _cartService.GetCart();
        if (cart == null || !cart.Any())
            return RedirectToAction("CartIndex");

        return View(new Order());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveCheckout(Order order)
    {
        var cart = _cartService.GetCart();
        if (cart == null || !cart.Any())
            return RedirectToAction("CartIndex");

        if (!ModelState.IsValid)
            return View("Checkout", order);

        var createdOrder = _orderService.CreateOrder(
            cart,
            order.CustomerName!,
            order.Address!,
            order.PhoneNumber!
        );

        _cartService.ClearCart();

        return RedirectToAction("OrderConfirmation", new { id = createdOrder.Id });
    }


    public IActionResult OrderConfirmation(int id)
    {
        var order = _orderService.GetOrderById(id);
        if (order == null) return NotFound();
        return View(order);
    }
}
