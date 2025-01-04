using eShopv1.Domain.Abstractions;
using MediatR;

namespace eShopV1.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    {
    }

}
