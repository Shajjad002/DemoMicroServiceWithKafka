using Microsoft.AspNetCore.Mvc;
using OrderAPI.OrderServices;
using Shared;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController(IOrderServices orderServices) : ControllerBase
    {
        [HttpGet("start-consuming-service")]
        public async Task<IActionResult> StartService()
        {

            await orderServices.StartConsumingService();
            return NoContent();
        }

        [HttpGet("get-product")]
        public IActionResult GetProducts()
        {
            var pruducts = orderServices.GetProducts();
            return Ok(pruducts);
        }

        [HttpPost("add-order")]
        public IActionResult AddOrder(Order model)
        {
            orderServices.AddOrder(model);
            return Ok("Order placed");

        }

        [HttpGet("order-summary")]
        public IActionResult GetOrderSummary()=> Ok(orderServices.GetOrderSummary());
    }
}
