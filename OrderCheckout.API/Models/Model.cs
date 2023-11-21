namespace OrderCheckout.API.Models
{

    public class Card
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public DateTime ValidTo { get; set; }
        public double amount { get; set; }
    }
    public class AdressInfo
    {
        public string Street { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class CartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
    public class Order
    {
        public Card card { get; set; }
        public AdressInfo adress { get; set; }
        public List<CartItem> Cartitems { get; set; }
    }

    public class ShipmentDetails
    {
        public string TrackingId { get; set; }

        public DateTime ShippingDate { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public string DeliveryPartner { get; set; }
    }

}
