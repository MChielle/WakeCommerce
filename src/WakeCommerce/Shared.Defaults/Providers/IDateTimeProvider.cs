namespace Shared.Defaults.Providers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
