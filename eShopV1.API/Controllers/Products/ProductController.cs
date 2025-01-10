using Asp.Versioning;
using eShopV1.Application.Products.GetProducts;
using eShopV1.Application.Products.GetProductDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShopV1.API.Controllers.Products
{
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
                request.Brand,
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
    }
}
