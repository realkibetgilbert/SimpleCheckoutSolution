using Moq;
using NUnit.Framework;
using OrderCheckout.API.Interfaces;
using OrderCheckout.API.Models;

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

        [Test]
        public void ChargeandShip_WithExpiredCard_ReturnsCardExpiredMessage()
        {
            // Arrange
            var order = new Order
            {
                Card = new Card
                {
                    amount = 100,
                    CardNumber = "1234567812345678",
                    ValidTo = DateTime.Now.AddDays(-1) // Expired card
                },
                Address = new AdressInfo()
            };

            // Act
            var result = _paymentService.ChargeandShip(order);

            const string EXPECTED = "Card Expired";
            // Assert
            Assert.That(result,Is.EqualTo(EXPECTED));
        }
        [Test]
        public void ChargeandShip_WithInvalidCardNumber_ReturnsCardNumberNotValidMessage()
        {
            // Arrange
            var order = new Order
            {
                Card = new Card
                {
                    amount = 100,
                    CardNumber = "12934", // Invalid card number
                    ValidTo = DateTime.Now.AddDays(30)
                },
                Address = new AdressInfo()
            };

            // Act
            var result = _paymentService.ChargeandShip(order);

            // Assert
            Assert.That(result, Is.EqualTo("Card Number is not valid"));
        }

        [Test]
        public void ChargeandShip_WithFailedPayment_ReturnsPaymentFailedMessage()
        {
            // Arrange
            var order = new Order
            {
                Card = new Card
                {
                    amount = 100,
                    CardNumber = "1234567812345678",
                    ValidTo = DateTime.Now.AddDays(30)
                },
                Address = new AdressInfo()
            };

            // Override the MakePayment method to return false (failed payment)
            var paymentServiceWithFailedPayment = new PaymentService(_shipmentServiceMock.Object);

            // Act
            var result = paymentServiceWithFailedPayment.ChargeandShip(order);

            // Assert
            Assert.That(result,Is.EqualTo("Payment Failed"));
        }
       
    }
}
