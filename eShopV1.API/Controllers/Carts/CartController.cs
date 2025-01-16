using Asp.Versioning;
using eShopV1.Application.Carts.CreateOrUpdateCart;
using eShopV1.Application.Carts.DeleteCart;
using eShopV1.Application.Carts.GetCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShopV1.API.Controllers.Carts
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/carts")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{cartId:guid}")]
        public async Task<IActionResult> GetCart(
            Guid cartId,
            CancellationToken cancellationToken)
        {
            var query = new GetCartQuery(cartId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.Code switch
                {
                    "ShoppingCart.NotFound" => NotFound(result.Error),
                    _ => BadRequest(result.Error)
                };
            }

            return Ok(result.Value);
        }

        [HttpGet("buyer/{buyerId:guid}")]
        public async Task<IActionResult> GetCartByBuyer(
            Guid buyerId,
            CancellationToken cancellationToken)
        {
            var query = new GetCartByBuyerQuery(buyerId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.Code switch
                {
                    "ShoppingCart.NotFound" => NotFound(result.Error),
                    _ => BadRequest(result.Error)
                };
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateCart(
            [FromBody] CreateOrUpdateCartRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateOrUpdateCartCommand(
                request.CartId,
                request.BuyerId,
                request.Items.Select(i => new CreateCartItemRequest(
                    i.ProductId,
                    i.ProductName,
                    i.Price,
                    i.Quantity,
                    i.PictureUrl,
                    i.Currency)).ToList(),
                request.Coupon is not null ? new Application.Carts.CreateOrUpdateCart.CouponRequest(
                    request.Coupon.Name,
                    request.Coupon.AmountOff,
                    request.Coupon.PercentOff,
                    request.Coupon.PromotionCode) : null,
                request.ClientSecret,
                request.PaymentIntentId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return result.Value.IsNew 
                ? Created($"/api/v1.0/carts/{result.Value.CartId}", result.Value)
                : Ok(result.Value);
        }

        [HttpDelete("{cartId:guid}")]
        public async Task<IActionResult> DeleteCart(
            Guid cartId,
            CancellationToken cancellationToken)
        {
            var command = new DeleteCartCommand(cartId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.Code switch
                {
                    "ShoppingCart.NotFound" => NotFound(result.Error),
                    _ => BadRequest(result.Error)
                };
            }

            return NoContent();
        }
    }
}
