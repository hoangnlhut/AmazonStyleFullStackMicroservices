using Discount.Commands;
using Discount.DTOs;
using Discount.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DiscountController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<CouponDto>> GetDiscountsByProductName(string productName)
        {
            var discounts = await _mediator.Send(new GetDiscountByProductNameQuery(productName));
            return Ok(discounts);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiscount([FromBody] CouponInput couponInput)
        {
            var command = new CreateDiscountCommand(couponInput);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDiscount([FromBody] CouponInput couponInput)
        {
            var getDiscountQuery = new GetDiscountByProductNameQuery(couponInput.ProductName);
            var discount = await _mediator.Send(getDiscountQuery);
            if (discount == null)
            {
                return NotFound();
            }

            couponInput.Id = discount.Id; // Set the Id of the couponInput to the existing discount's Id

            var command = new UpdateDiscountCommand(couponInput);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{productName}")]
        public async Task<ActionResult> DeleteDiscount(string productName)
        {
            var getDiscountQuery = new GetDiscountByProductNameQuery(productName);
            var discount = await _mediator.Send(getDiscountQuery);
            if (discount == null)
            {
                return NotFound();
            }

            var command = new DeleteDiscountCommand(productName);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
