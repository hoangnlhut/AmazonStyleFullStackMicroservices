using Basket.DTOs;
using Basket.Queries;
using Basket.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{userName}")]
        public async Task<ActionResult<IList<BasketDto>>> GetBasketByUsername(string userName)
        {
            var query = new GetBasketByUsernameQuery(userName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
