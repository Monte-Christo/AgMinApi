namespace MinApi;

public class BirthDayService(TimeProvider timeProvider)
{
  public string BirthdayCheck(DateTime birthDate)
  {
    var month = timeProvider.GetUtcNow().Month;
    var day = timeProvider.GetUtcNow().Day;
    var yesOrNo = month == birthDate.Month && day == birthDate.Day ? String.Empty : "not ";
    return $" Today is {yesOrNo}your birthday";
  }
}