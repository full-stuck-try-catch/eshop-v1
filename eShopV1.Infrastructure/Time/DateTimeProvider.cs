using eShopV1.Application.Abstractions.Time;

namespace eShopV1.Infrastructure.Time
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
