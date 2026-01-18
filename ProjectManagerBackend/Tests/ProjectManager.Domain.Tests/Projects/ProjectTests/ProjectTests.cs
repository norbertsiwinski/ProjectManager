using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.Projects;
using ProjectManager.Domain.TaskItems;

namespace ProjectManager.Domain.Tests.Projects.ProjectTests;

public class ProjectTests
{
    [Fact]
    public void Create_WithValidName_ShouldCreateProject()
    {
        // Arrange
        var name = new ProjectName("name");

        // Act
        var project = Project.Create(name);

        // Assert
        Assert.NotNull(project);
        Assert.Equal("name", project.Name.Value);
    }

    [Fact]
    public void AddTask_WithValidName_ShouldAddTaskToTasksCollection()
    {
        // Arrange
        var name = new ProjectName("name");
        var project = Project.Create(name);

        // Act
        var task = project.AddTask(new TaskName("task 1"));

        // Assert
        Assert.NotNull(task);
        Assert.Contains(task, project.Tasks);
        Assert.Equal(project.Id, task.ProjectId);
    }

    [Fact]
    public void AddMember_WithUniqueUserId_ShouldAddMemberToMembersCollection()
    {
        // Arrange
        var project = Project.Create(new ProjectName("name"));
        var userId = Guid.NewGuid();

        // Act
        var member = project.AddMember(userId);

        // Assert
        Assert.Contains(member, project.Members);
        Assert.Equal(userId, member.UserId);
    }

    [Fact]
    public void AddMember_WithExistingUserId_ShouldThrowDomainException()
    {
        // Arrange
        var project = Project.Create(new ProjectName("name"));
        var userId = Guid.NewGuid();

        //Act
        var member = project.AddMember(userId);

        // Assert
        Assert.Throws<DomainException>(() => project.AddMember(userId));
        Assert.Contains(member, project.Members);
        Assert.Single(project.Members, m => m.UserId == userId);
    }
}