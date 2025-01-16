using Asp.Versioning;
using eShopV1.Application.CartGuests.DeleteGuestCart;
using eShopV1.Application.CartGuests.GetGuestCart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopV1.API.Controllers.ShopppingCartGuests
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/cartguests")]
    public class CartGuestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartGuestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateGuestCart(
            [FromBody] UpdateGuestCartRequest request,
            CancellationToken cancellationToken)
        {
            var query = new UpdateGuestCartQuery(request.CartId, request.Items.Select(i => new GuestCartItemQueryRequest(
                i.ProductId,
                i.Quantity,
                i.ProductName,
                i.Price,
                i.PictureUrl,
                i.Currency,
                i.Brand,
                i.Type)).ToList());

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetGuestCart(
            string cartId,
            CancellationToken cancellationToken)
        {
            var query = new GetGuestCartQuery(cartId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error.Code, message = result.Error.Name });
            }

            return Ok(result.Value);
        }

        [Authorize]
        [HttpDelete("{guestId}")]
        public async Task<IActionResult> DeleteCart(
            string guestId,
            CancellationToken cancellationToken)
        {
            var command = new DeleteGuestCartCommand(guestId);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }
    }
}
