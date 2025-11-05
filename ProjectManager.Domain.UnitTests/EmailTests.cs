using ProjectManager.Domain.Users;

namespace ProjectManager.Domain.UnitTests;

public class EmailTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_Should_ThrowArgumentException_WhenValueIsInvalid(string? value)
    {
        //new User
    }
}