using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OrderCheckout.API.Interfaces;
using OrderCheckout.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCheckout.UnitTests.ServicesTests
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<IPaymentService> _paymentServiceMock;
        private CartService _cartService;

        [SetUp]
        public void SetUp()
        {
            _paymentServiceMock= new Mock<IPaymentService>();
            _cartService = new CartService(_paymentServiceMock.Object);
        }

        [Test]
        public void ValidateCart_WithInvalidCartItemCount_ReturnInvalidCartMessage()
        {
            //Arrange
            var order = new Order
            {
                Cartitems = new List<CartItem>(),//empty list
                Card = new Card(),
                Address = new AdressInfo()
            };

            //Act
            var result= _cartService.ValidateCart(order);

            //Assert

            Assert.That(result, Is.EqualTo("Invalid Cart"));
        }

    }
}
