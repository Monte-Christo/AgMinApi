namespace MinApi.Tests;

public interface IDateTimeProvider
{
  DateTime UtcNow { get; }
}