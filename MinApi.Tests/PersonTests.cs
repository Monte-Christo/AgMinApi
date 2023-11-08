using Moq;

namespace MinApi.Tests;

public class PersonTests
{
  [Fact]
  public void BirthdayCheck_ReturnsNonBirthdayMessage()
  {
      var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
      mockedDateTimeProvider.SetupGet(m => m.UtcNow).Returns(new DateTime(2021, 6, 21));

    var p = new Person("Edgar", "Knapp", DateTime.Parse("November 8, 1959"));
    Assert.Equal(" Today is not your birthday", p.BirthdayCheck(mockedDateTimeProvider.Object));
  }

  [Fact]
  public void BirthdayCheck_ReturnsBirthdayMessage()
  {
    var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
    mockedDateTimeProvider.SetupGet(m => m.UtcNow).Returns(new DateTime(2021, 6, 21));

    var p = new Person("Edgar", "Knapp", DateTime.Parse("June 21, 1959"));
    Assert.Equal(" Today is your birthday", p.BirthdayCheck(mockedDateTimeProvider.Object));
  }

}