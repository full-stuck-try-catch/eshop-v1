using eShopv1.Domain.Abstractions;
using MediatR;

namespace eShopV1.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
