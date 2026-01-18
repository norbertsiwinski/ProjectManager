using Moq;
using ProjectManager.Application.Abstractions;
using ProjectManager.Application.Projects.Commands;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Application.Tests.Projects.Commands;

public class CreateProjectCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

    [Fact]
    public async Task Handle_Should_ReturnException_WhenNameIsEmpty()
    {
        // Arrange
        var command = new CreateProjectCommand(string.Empty);

        var handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act 
        Task Act() => handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(Act);
    }

    [Fact]
    public async Task Handle_Should_ReturnProjectId_WhenNameIsValid()
    {
        // Arrange
        var command = new CreateProjectCommand("test");
        var handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _projectRepositoryMock.Verify(pr => pr.Add(It.Is<Project>(p => p.Id == result)), Times.Once);

        _unitOfWorkMock.Verify(
            u => u.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WhenNameIsEmpty_ShouldThrow_AndNotCallRepoOrUoW()
    {
        // Arrange
        var command = new CreateProjectCommand(string.Empty);
        var handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        Task Act() => handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(Act);

        _projectRepositoryMock.Verify(r => r.Add(It.IsAny<Project>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenSaveChangesThrows_ShouldPropagateException()
    {
        // Arrange
        var command = new CreateProjectCommand("test");

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("db error"));

        var handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        Task Act() => handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(Act);

        _projectRepositoryMock.Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
    }
}