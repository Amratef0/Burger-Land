using ReservationSystem.Models;
using System.Threading.Tasks;

public interface ICartService
{
    List<CartItem> GetCart();
    Task AddToCart(int productId);
    void RemoveFromCart(int productId);
    void IncreaseQuantity(int productId);
    void DecreaseQuantity(int productId);
    public void ClearCart();

}
