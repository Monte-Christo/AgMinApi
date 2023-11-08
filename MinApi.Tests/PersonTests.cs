using Moq;

namespace MinApi.Tests;

public class PersonTests
{
  [Fact]
  public void BirthdayCheck_ReturnsBirthdayMessage()
  {
      var mock = new Mock<IDateTime>();
      mock.SetupGet(m => m.UtcNow).Returns(new DateTime(2021, 6, 21));

    var p = new Person("Edgar", "Knapp", DateTime.Parse("November 7, 1959"));
    Assert.Equal(" Today is not your birthday", p.BirthdayCheck());
  }
}

public interface IDateTime
{
    DateTime UtcNow { get; }
}
