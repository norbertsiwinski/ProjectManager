using FluentValidation;

namespace ProjectManager.Application.TaskItems.Commands.CreateTaskItem;

public class CreateTaskItemValidator : AbstractValidator<CreateTaskItemCommand>
{
    public CreateTaskItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Task item name is required.")
            .MaximumLength(50).WithMessage("Task item name must not exceed 50 characters.");
    }
}
