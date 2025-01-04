using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Application.Abstractions.Time
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
