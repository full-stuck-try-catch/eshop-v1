using eShopv1.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
    {
    }

    public interface IBaseCommand
    {
    }
}
