using ReservationSystem.Models;

public interface IOrderService
{
    Order CreateOrder(List<CartItem> cart, string customerName, string address, string phoneNumber);
    Order GetOrderById(int id);
}
