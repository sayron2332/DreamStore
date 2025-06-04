using DreamStore.Core.Dtos.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrderDto createOrderDto)
        {
            return Ok();
        }

        [HttpPost("confirm-order/{id:int}")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            return Ok();
        }
    }
}
