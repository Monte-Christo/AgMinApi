namespace MinApi;

public record Person(string FirstName, string LastName, DateTime BirthDate)
{
  public string BirthdayCheck()
  {
    var month = DateTime.UtcNow.Month;
    var day = DateTime.UtcNow.Day;
    var yesOrNo = month == BirthDate.Month && day == BirthDate.Day ? String.Empty : "not ";
    return $" Today is {yesOrNo}your birthday";
  }
}
