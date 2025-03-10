using FluentValidation;

namespace EmployeeManagement.Application.Cafes.Commands.Patch;
public class PatchCafeCommandValidator : AbstractValidator<PatchCafeCommand>
{
    public PatchCafeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(10)
            .WithMessage("Name cannot be empty and must be between 2 and 10 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(250)
            .WithMessage("Description cannot be empty and must be less than 250 characters");

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Location cannot be empty and must be less than 100 characters");
    }
}
