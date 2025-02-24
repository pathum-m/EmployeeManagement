using FluentValidation;

namespace EmployeeManagement.Application.Cafes.Commands.Create;
internal class CreateCafeCommandValidator : AbstractValidator<CreateCafeCommand>
{
    public CreateCafeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100)
            .WithMessage("Name cannot be empty and must be between 2 and 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Description cannot be empty and must be less than 500 characters");

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Location cannot be empty and must be less than 200 characters");
    }
}
