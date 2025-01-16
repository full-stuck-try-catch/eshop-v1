using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopv1.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Products.GetBrands;

internal sealed class GetBrandsQueryHandler : IQueryHandler<GetBrandsQuery, List<string>>
{
    private readonly IApplicationDbContext _context;

    public GetBrandsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<string>>> Handle(
        GetBrandsQuery request, 
        CancellationToken cancellationToken)
    {
        var brands = await _context.Products
            .AsNoTracking()
            .Where(p => !string.IsNullOrEmpty(p.Brand))
            .Select(p => p.Brand)
            .Distinct()
            .OrderBy(b => b)
            .ToListAsync(cancellationToken);

        return Result.Success(brands);
    }
}