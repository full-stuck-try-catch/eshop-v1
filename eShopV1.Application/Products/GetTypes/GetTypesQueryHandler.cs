using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopv1.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Products.GetTypes;

internal sealed class GetTypesQueryHandler : IQueryHandler<GetTypesQuery, List<string>>
{
    private readonly IApplicationDbContext _context;

    public GetTypesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<string>>> Handle(
        GetTypesQuery request, 
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