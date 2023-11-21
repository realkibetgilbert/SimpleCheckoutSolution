using OrderCheckout.API.Models;

namespace OrderCheckout.API.Interfaces
{
    public class PaymentService : IPaymentService
    {
        private readonly IShipmentService _shipmentService;

        public PaymentService(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }
        public string ChargeandShip(Order order)
        {
            if (order.card.amount <= 0)
            {
                return "Amount is not valid";
            }

            if (order.card != null)
            {
                if (order.card.ValidTo < DateTime.Now)
                    return "Card Expired";
                if (order.card.CardNumber.Length < 16)
                    return "Card Number is not valid";
            }

            bool makePayment = MakePayment(order.card);

            if (makePayment)
            {
                var shipment = _shipmentService.Ship(order.adress);

                if (shipment != null)
                    return "Item Shipped";
                else
                    return "Something went wrong with the shipment";

            }
            else
            {
                return "Payment Failed";
            }
        }

        public virtual bool MakePayment(Card card)
        {
            //calling thirdparty api to make payment and debit the card
            return true;
        }
    }
}
