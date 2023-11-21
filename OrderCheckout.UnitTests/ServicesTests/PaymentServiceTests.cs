using Moq;
using NUnit.Framework;
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
    public class PaymentServiceTests
    {
        private Mock<IShipmentService> _shipmentServiceMock;
        private PaymentService _paymentService;

        [SetUp]
        public void SetUp()
        {
            _shipmentServiceMock = new Mock<IShipmentService>();
            _paymentService = new PaymentService(_shipmentServiceMock.Object);
        }

        [Test]
        public void ChargeandShip_WithInvalidAmount_ReturnsAmountNotValidMessage()
        {
            // Arrange
            var order = new Order
            {
                Card = new Card
                {
                    amount = 0,
                    CardNumber = "1234567812345678",
                    ValidTo = DateTime.Now.AddDays(30)
                },
                Address = new AdressInfo()
            };

            // Act
            var result = _paymentService.ChargeandShip(order);

            // Assert
            Assert.That(result, Is.EqualTo("Amount is not valid"));
        }

    }
}
