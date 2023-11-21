using OrderCheckout.API.Models;

namespace OrderCheckout.API.Interfaces
{
    public interface IPaymentService
    {
        string ChargeandShip(Order order);
    }
}
