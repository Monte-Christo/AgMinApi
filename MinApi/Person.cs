using MinApi.Tests;

namespace MinApi;

public record Person(string FirstName, string LastName, DateTime BirthDate)
{
  public string BirthdayCheck(IDateTimeProvider dateTimeProvider)
  {
    var today = dateTimeProvider.UtcNow;
    var yesOrNo = today.Month == BirthDate.Month && today.Day == BirthDate.Day ? String.Empty : "not ";
    return $" Today is {yesOrNo}your birthday";
  }
}
