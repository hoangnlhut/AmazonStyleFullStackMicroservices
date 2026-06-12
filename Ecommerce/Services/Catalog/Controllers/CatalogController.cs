using Catalog.DTOs;
using Catalog.Queries;
using Catalog.Mappers;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog.Commands;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Products
        [HttpGet("products")]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            //return Ok(result.ToPaginationDtos());
            return Ok(result);
        }


        [HttpGet("products/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
            //return Ok(result.ToDto());
        }

        [HttpGet("products-by-name/{productName}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsByProductName(string productName)
        {
            var query = new GetProductsByNameQuery(productName);
            var result = await _mediator.Send(query);

            if (result == null || result.Any())
            {
                return NotFound();
            }

            return Ok(result.ToDtos());
        }

        [HttpGet("brands/{brand}/products")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsByBrand(string brand)
        {
            var query = new GetProductsByBrandQuery(brand);
            var result = await _mediator.Send(query);
            return Ok(result);
            //return Ok(result.ToDtos());
        }

        [HttpPost("products")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpPut("products/{id}")]
        public async Task<ActionResult<bool>> UpdateProduct(string id, [FromBody] UpdateProductDto productDto)
        {
            if (id == null || productDto == null) return BadRequest();

            var updateCommand = productDto.ToUpdateProductCommand(id);

            var result = await _mediator.Send(updateCommand);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("products/{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var command = new DeleteProductByIdCommand(id);
            var result = await _mediator.Send(command);

            if(!result)             {
                return NotFound();
            }

            return NoContent();
        }

        #endregion

        #region Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            //return Ok(result.ToDtos());
            return Ok(result);
        }
        #endregion

        #region Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            //return Ok(result.ToDtos());
            return Ok(result);

        }
        #endregion
    }
}
