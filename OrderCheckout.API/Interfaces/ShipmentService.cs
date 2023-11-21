using OrderCheckout.API.Models;

namespace OrderCheckout.API.Interfaces
{
    public class ShipmentService : IShipmentService
    {
        public ShipmentDetails Ship(AdressInfo info)
        {
            //Calling external api but wecan mock

            return new ShipmentDetails();
        }
    }
}
