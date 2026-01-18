namespace ProjectManager.Application.Exceptions;

public class NotFoundException(string resourceType) : ApplicationException($"{resourceType} does not exists!");