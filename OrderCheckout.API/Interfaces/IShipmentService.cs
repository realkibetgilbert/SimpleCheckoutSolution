using OrderCheckout.API.Models;

namespace OrderCheckout.API.Interfaces
{
    public interface IShipmentService
    {
        ShipmentDetails Ship(AdressInfo info);
    }
}
