using EmployeeManagement.Application.Cafes.Commands.Create;
using FluentValidation;

namespace EmployeeManagement.Application.Employees.Commands.Create;
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100)
            .WithMessage("Name cannot be empty");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("EmailAddress cannot be empty");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(15)
            .WithMessage("PhoneNumber cannot be empty");
    }
}
