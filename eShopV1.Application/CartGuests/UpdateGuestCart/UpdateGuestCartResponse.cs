namespace eShopV1.Application.CartGuests.GetGuestCart;

public sealed record UpdateGuestCartResponse(
    string CartId,
    List<UpdateGuestCartResoneItem> Items)
    {
        public static UpdateGuestCartResponse Create(string cartId, List<UpdateGuestCartResoneItem> items)
        {
            return new UpdateGuestCartResponse(cartId, items);
        }
    }

public sealed record UpdateGuestCartResoneItem(
    Guid ProductId,
    int Quantity, string ProductName, decimal Price, string PictureUrl,string Currency, string Brand, string Type);