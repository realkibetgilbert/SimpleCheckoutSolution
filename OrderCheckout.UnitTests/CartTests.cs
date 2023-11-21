using Moq;
using NUnit.Framework;
using OrderCheckout.API.Interfaces;

namespace OrderCheckout.UnitTests
{
    public class CartTests
    {
        private ICartService _cartService;
        private Mock<PaymentService> _paymentService;
        private Mock<ShipmentService> _shipmentService;

        [SetUp]
        public void SetUp()
        {
            _shipmentService = new Mock<ShipmentService>(); 
            _paymentService = new Mock<PaymentService>(_shipmentService.Object);
            _cartService= new CartService(_paymentService.Object);
        }
    }
}
