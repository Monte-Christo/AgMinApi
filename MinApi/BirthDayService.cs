namespace MinApi;

public class BirthDayService
{
  private readonly TimeProvider _timeProvider;

  public BirthDayService(TimeProvider timeProvider)
  {
    _timeProvider = timeProvider;
  }

  public string BirthdayCheck(DateTime birthDate)
  {
    var month = _timeProvider.GetUtcNow().Month;
    var day = _timeProvider.GetUtcNow().Day;
    var yesOrNo = month == birthDate.Month && day == birthDate.Day ? String.Empty : "not ";
    return $" Today is {yesOrNo}your birthday";
  }
}