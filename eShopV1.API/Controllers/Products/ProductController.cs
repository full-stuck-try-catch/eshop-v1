using Asp.Versioning;
using eShopV1.Application.Products.GetProducts;
using eShopV1.Application.Products.GetProductDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using eShopV1.Application.Products.GetBrands;
using eShopV1.Application.Products.GetTypes;
using Microsoft.AspNetCore.Authorization;

namespace eShopV1.API.Controllers.Products
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
      
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] GetProductsRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetProductsQuery(
                request.SearchTerm,
                request.Brands,
                request.Types,
                request.MinPrice,
                request.MaxPrice,
                request.Status,
                request.SortBy,
                request.SortOrder,
                request.Page,
                request.PageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductDetail(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetProductDetailQuery(id);

                var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.Code switch
                {
                    "Product.NotFound" => NotFound(result.Error),
                    _ => BadRequest(result.Error)
                };
            }

            return Ok(result.Value);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands(CancellationToken cancellationToken)
        {
            var query = new GetBrandsCachedQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTypes(CancellationToken cancellationToken)
        {
            var query = new GetTypesCachedQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
