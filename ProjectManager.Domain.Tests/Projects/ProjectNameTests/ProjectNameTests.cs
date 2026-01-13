using ProjectManager.Domain.Projects;

namespace ProjectManager.Domain.Tests.Projects.ProjectNameTests;

public class ProjectNameTests
{

    [Fact]
    public void Create_WithEmptyName_ShouldThrowArgumentNullException()
    {
        Action a = () => Project.Create(new ProjectName(""));

        Assert.Throws<ArgumentNullException>(() => a());
    }

    [Fact]
    public void Create_WithWhitespaceName_ShouldThrowArgumentNullException()
    {
        Action a = () => Project.Create(new ProjectName("   "));
        Assert.Throws<ArgumentNullException>(() => a());
    }

    [Fact]
    public void Ctor_WithValidValue_ShouldSetValue()
    {
        var name = new ProjectName("name");

        Assert.Equal("name", name.Value);
        Assert.NotNull(name.Value);
    }
}