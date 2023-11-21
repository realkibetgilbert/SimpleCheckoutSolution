using Microsoft.AspNetCore.Cors.Infrastructure;
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
        [Test]
        public void ValideCart_WithInvalidProductQuantity_ReturnsInvalidQuantityMessage()
        {
            //Arrange
            var order = new Order
            {
                Cartitems = new List<CartItem>
                {
                    new CartItem { Quantity = -5, Price = 20 } // Invalid quantity
                },
                Card = new Card(),
                Address = new AdressInfo()
            };

            //Act
            var result = _cartService.ValidateCart(order);
            //Assert
            Assert.That(result, Is.EqualTo("Invalid Product Quantity"));
        }

        public void ValideCart_WithInvalidProductQuantity_ReturnInvalidQuantityMessage()
        {
            // Arrange
            var order = new Order
            {
                Cartitems = new List<CartItem>
                {
                    new CartItem { Quantity = 15, Price = 20 } // Quantity greater than 10
                },
                Card = new Card(),
                Address = new AdressInfo()
            };

            // Act
            var result = _cartService.ValidateCart(order);

            // Assert
            Assert.That(result, Is.EqualTo("Invalid Product Quantity"));

        }

        [Test]
        public void ValidateCart_WithValidCart_CallsChargeAndShipMethod()
        {
            // Arrange
            var order = new Order
            {
                Cartitems = new List<CartItem>
                {
                    new CartItem { Quantity = 5, Price = 20 }
                }
            };

            // Act
            var result = _cartService.ValidateCart(order);

            // Assert
            _paymentServiceMock.Verify(x => x.ChargeandShip(order), Times.Once);
        }

        [Test]
        public void ValidateCart_WithPaymentServiceFailure_ReturnsPaymentServiceFailureMessage()
        {
            // Arrange
            var order = new Order
            {
                Cartitems = new List<CartItem>
                {
                    new CartItem { Quantity = 5, Price = 20 }
                },
                Card = new Card(),
                Address = new AdressInfo()
            };

            // Set up mock to simulate payment service failure
            _paymentServiceMock.Setup(x => x.ChargeandShip(It.IsAny<Order>())).Returns("Payment Failed");

            // Act and Assert
            var result = _cartService.ValidateCart(order);

            // Assert
            Assert.That(result, Is.EqualTo("Payment Failed"));
        }
    }
}
