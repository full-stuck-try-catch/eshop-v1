using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eShopv1.Domain.Users
{
    public sealed record Address(string Name, string Line1, string? Line2, string City, string State, string PostalCode, string Country);
}
