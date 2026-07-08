using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Mappers;
using Ordering.Queries;

namespace Ordering.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{userName}", Name = "GetOrdersByUserName" )]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderList(string userName)
        {
            var orders = await _mediator.Send(new GetOrderListQuery(userName));
            _logger.LogInformation($"Orders retrieved for user: {userName}, Count: {orders.Count}");
            return Ok(orders);
        }

        [HttpPost(Name = "CheckoutOrder")]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var result = await _mediator.Send(dto.ToCommand());
            _logger.LogInformation($"Order created with ID: {result}");
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        public async Task<ActionResult> UpdateOrder([FromBody] OrderDto dto)
        {
            if (dto.Id <= 0 || dto == null) return BadRequest();
            await _mediator.Send(dto.ToCommand());
            _logger.LogInformation($"Order updated with ID: {dto.Id}");
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand(id);
            await _mediator.Send(command);
            _logger.LogInformation($"Order deleted with ID: {id}");
            return NoContent();
        }

    }
}
