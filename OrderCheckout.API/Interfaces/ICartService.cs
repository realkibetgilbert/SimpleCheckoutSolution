using OrderCheckout.API.Models;

namespace OrderCheckout.API.Interfaces
{
    public interface ICartService
    {
        string ValidateCart(Order order);
    }
}
