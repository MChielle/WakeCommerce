using Shared.Defaults.Providers;

namespace Catalog.Infrastructure.Providers
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}