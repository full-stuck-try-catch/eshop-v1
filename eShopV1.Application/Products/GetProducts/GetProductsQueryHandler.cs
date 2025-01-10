using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Pagination;
using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Products.GetProducts;

internal sealed class GetProductsQueryHandler 
    : IQueryHandler<GetProductsQuery, PagedList<ProductResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedList<ProductResponse>>> Handle(
        GetProductsQuery request, 
        CancellationToken cancellationToken)
    {
        IQueryable<Product> query = _context.Products.AsNoTracking();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searchTerm = request.SearchTerm.ToLower();
            query = query.Where(p => 
                p.Name.ToLower().Contains(searchTerm) || 
                p.Description.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(request.Brand))
        {
            query = query.Where(p => p.Brand.ToLower() == request.Brand.ToLower());
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= request.MaxPrice.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(p => (int)p.Status == request.Status.Value);
        }

        // Apply sorting
        query = ApplySorting(query, request.SortBy, request.SortOrder);

        // Project to response
        var projectedQuery = query.Select(p => new ProductResponse(
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            p.Currency,
            p.PictureUrl,
            p.Brand,
            p.QuantityInStock,
            (int)p.Status,
            p.CreatedAt,
            p.UpdatedAt));

        // Apply pagination
        var pagedProducts = await PagedList<ProductResponse>.CreateAsync(
            projectedQuery,
            request.Page,
            request.PageSize);

        return Result.Success(pagedProducts);
    }

    private static IQueryable<Product> ApplySorting(
        IQueryable<Product> query,
        string? sortBy,
        string? sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            return query.OrderBy(p => p.Name);
        }

        var isDescending = sortOrder?.ToLower() == "desc";

        return sortBy.ToLower() switch
        {
            "name" => isDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            "price" => isDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
            "brand" => isDescending ? query.OrderByDescending(p => p.Brand) : query.OrderBy(p => p.Brand),
            "createdat" => isDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
            "quantityinstock" => isDescending ? query.OrderByDescending(p => p.QuantityInStock) : query.OrderBy(p => p.QuantityInStock),
            "status" => isDescending ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status),
            _ => query.OrderBy(p => p.Name)
        };
    }
}