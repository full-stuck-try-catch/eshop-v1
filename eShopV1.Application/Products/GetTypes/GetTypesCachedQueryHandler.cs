using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopv1.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Products.GetTypes;

internal sealed class GetTypesCachedQueryHandler : IQueryHandler<GetTypesCachedQuery, List<string>>
{
    private readonly IApplicationDbContext _context;

    public GetTypesCachedQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<string>>> Handle(
        GetTypesCachedQuery request, 
        CancellationToken cancellationToken)
    {
        var types = await _context.Products
            .AsNoTracking()
            .Where(p => !string.IsNullOrEmpty(p.Type))
            .Select(p => p.Type)
            .Distinct()
            .OrderBy(t => t)
            .ToListAsync(cancellationToken);

        return Result.Success(types);
    }
}