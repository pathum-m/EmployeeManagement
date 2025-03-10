using FluentValidation;

namespace EmployeeManagement.Application.Employees.Commands.Patch;
public class PatchEmployeeCommandValidator : AbstractValidator<PatchEmployeeCommand>
{
    public PatchEmployeeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(10)
            .WithMessage("Name cannot be empty");

        RuleFor(x => x.EmailAddress)
            .NotNull()
            .WithMessage("Email must be provided");

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .WithMessage("Phone number must be provided");

        RuleFor(x => x.Gender)
            .NotNull()
            .WithMessage("Gender must be provided");
    }
}
