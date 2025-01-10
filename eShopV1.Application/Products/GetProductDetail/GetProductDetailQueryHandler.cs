using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Products.GetProductDetail;

internal sealed class GetProductDetailQueryHandler 
    : IQueryHandler<GetProductDetailQuery, ProductDetailResponse>
{
    private readonly IApplicationDbContext _context;

    public GetProductDetailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductDetailResponse>> Handle(
        GetProductDetailQuery request, 
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Where(p => p.Id == request.ProductId)
            .Select(p => new ProductDetailResponse(
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.Currency,
                p.PictureUrl,
                p.Brand,
                p.QuantityInStock,
                p.Status.ToString(),
                p.Status == ProductStatus.Published && p.QuantityInStock > 0,
                p.CreatedAt,
                p.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductDetailResponse>(ProductErrors.NotFound);
        }

        return Result.Success(product);
    }
}