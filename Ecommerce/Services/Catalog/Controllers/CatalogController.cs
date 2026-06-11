using Catalog.DTOs;
using Catalog.Queries;
using Catalog.Mappers;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("products")]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            //return Ok(result.ToPaginationDtos());
            return Ok(result);
        }


        [HttpGet("products/{id:string}")]
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


        [HttpGet("products-by-brand/{brand}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsByBrand(string brand)
        {
            var query = new GetProductsByBrandQuery(brand);
            var result = await _mediator.Send(query);
            return Ok(result.ToDtos());
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IList<ProductBrandDto>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result.ToDtos());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IList<ProductTypeDto>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result.ToDtos());
        }
    }
}
