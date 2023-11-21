using OrderCheckout.API.Models;

namespace OrderCheckout.API.Interfaces
{
    public class CartService : ICartService
    {
        private readonly IPaymentService _paymentService;

        public CartService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public string ValidateCart(Order order)
        {
            if (order.Cartitems.Count < 0)
                return "Invalid Cart";

            if (order.Cartitems.Any(x => x.Quantity < 0 || x.Quantity > 10))
                return ("Invalid Product Quantity");

            return _paymentService.ChargeandShip(order);
        }
    }
}
