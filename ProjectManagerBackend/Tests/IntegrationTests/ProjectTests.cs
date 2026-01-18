using FluentValidation;
using ProjectManager.Application.Projects.Commands;
using ProjectManager.Application.Projects.Queries.GetProject;

namespace ProjectManager.IntegrationTests;

public class ProjectTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateProject_WhenProductNameIsNull_ShouldReturnArgumentNullException()
    {
        // Arrange
        var command = new CreateProjectCommand("");

        // Act
         Task Act() => sender.Send(command);

        // Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(Act);
        Assert.Contains("Project name is required", ex.Message);
    }

    [Fact]
    public async Task CreateProject_WhenProductNameIsValid_ShouldAddNewProjectToDatabase()
    {
        // Arrange
        var command = new CreateProjectCommand("New Project");

        // Act
        var projectId = await sender.Send(command);

        // Assert
        var project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);
        Assert.NotNull(project);
    }

    [Fact]
    public async Task GetProjectById_WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        var command = new CreateProjectCommand("New Project");
        var projectId = await sender.Send(command);
        var query = new GetProjectQuery(projectId);

        // Act
        var project = await sender.Send(query);

        // Assert
        Assert.NotNull(project);
        Assert.Equal(project.Id, projectId);
    }
}