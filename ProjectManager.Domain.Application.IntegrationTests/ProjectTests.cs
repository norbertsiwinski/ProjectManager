using ProjectManager.Application.Projects.Commands;

namespace ProjectManager.Application.IntegrationTests;

public class ProjectTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateProject_ShouldReturnValidId()
    {
        // Arrange
        var command = new CreateProjectCommand("New Project");

        // Act
        var projectId = await sender.Send(command);

        // Assert
        Assert.NotEqual(Guid.Empty, projectId);
    }
}