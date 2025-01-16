﻿namespace eShopV1.API.Controllers.ShopppingCartGuests
{
    public sealed class UpdateGuestCartRequest
    {
        public string CartId { get; set; }
        public List<UpdateGuestCartItemRequest> Items { get; set; }
    }

    public sealed class UpdateGuestCartItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Currency { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
