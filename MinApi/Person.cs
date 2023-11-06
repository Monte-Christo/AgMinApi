namespace MinApi;

public record Person(string FirstName, string LastName, DateTime BirthDate)
{
  public string BirthdayCheck(TimeProvider tp)
  {
    var month = tp.GetUtcNow().Month;
    var day = tp.GetUtcNow().Day;
    var yesOrNo = month == BirthDate.Month && day == BirthDate.Day ? String.Empty : "not ";
    return $" Today is {yesOrNo}your birthday";
  }
}