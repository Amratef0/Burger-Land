using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReservationSystem.Data;
using ReservationSystem.Models;
using System.Collections.Generic;
using System.Linq;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartSessionKey = "Cart";

    public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public List<CartItem> GetCart()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cartJson = session.GetString(CartSessionKey);
        return cartJson != null ? JsonConvert.DeserializeObject<List<CartItem>>(cartJson) : new List<CartItem>();
    }

    public void SaveCart(List<CartItem> cart)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cartJson = JsonConvert.SerializeObject(cart);
        session.SetString(CartSessionKey, cartJson);
    }

    public async Task AddToCart(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return;

        var cart = GetCart();
        var existingItem = cart.FirstOrDefault(c => c.Product.Id == productId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem { Product = product, Quantity = 1 });
        }

        SaveCart(cart);
    }

    public void RemoveFromCart(int productId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null)
        {
            cart.Remove(item);
            SaveCart(cart);
        }
    }

    public void IncreaseQuantity(int productId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null)
        {
            item.Quantity++;
            SaveCart(cart);
        }
    }

    public void DecreaseQuantity(int productId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null && item.Quantity > 1)
        {
            item.Quantity--;
            SaveCart(cart);
        }
    }
    public void ClearCart()
    {
        _httpContextAccessor.HttpContext!.Session.Remove("Cart");
    }



}
