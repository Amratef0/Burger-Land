using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Order CreateOrder(List<CartItem> cart, string customerName, string address, string phoneNumber)
    {
        var order = new Order
        {
            CustomerName = customerName,
            Address = address,
            PhoneNumber = phoneNumber,
            OrderDate = DateTime.Now,
            TotalAmount = cart.Sum(item => (item.Product.Price ?? 0) * item.Quantity),
            OrderItems = cart.Select(item => new OrderItem
            {
                ProductId = item.Product.Id,
                Quantity = item.Quantity,
                Price = (decimal)(item.Product.Price ?? 0)
            }).ToList()
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        return order;
    }

    public Order GetOrderById(int id)
    {
        return _context.Orders
            .Where(o => o.Id == id)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefault();
    }
}
