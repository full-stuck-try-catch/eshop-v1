﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopv1.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
    }
}
