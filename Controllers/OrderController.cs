using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repopattern.Model;
using Repopattern.Repository.Interface;
using Repopattern.Service;

namespace Repopattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService order)
        {
           orderService = order;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var result = await orderService.CreateOrderAsync(request);

                return Ok(new
                {
                    OrderId = result.OrderId,
                    TotalAmount = result.TotalAmount
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
