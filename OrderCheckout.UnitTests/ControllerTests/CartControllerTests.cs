using Moq;
using NUnit.Framework;
using OrderCheckout.API.Controllers;
using OrderCheckout.API.Interfaces;

namespace OrderCheckout.UnitTests.ControllerTests
{
    [TestFixture]
    public class CartControllerTests
    {
        private Mock<ICartService> _cartService;
        private CartController _cartController;

        [SetUp]
        public void SetUp()
        {
            _cartService = new Mock<ICartService>();
            _cartController = new CartController(_cartService.Object);
        }

        [Test]
        public void CheckOut_WithNullOrder_ReturnsOrderIsNullMessage()
        {
            //Act
            var result = _cartController.CheckOut(null);

            //Assert
     
            Assert.That(result, Is.EqualTo("Order is null"));
        }
    }
}
