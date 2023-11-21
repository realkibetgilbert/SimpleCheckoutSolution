using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderCheckout.API.Interfaces;
using OrderCheckout.API.Models;

namespace OrderCheckout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public string CheckOut(Order order)
        {
            if (order == null) return "Order is null";

            var result = _cartService.ValidateCart(order);

            return result;
        }
    }
}
