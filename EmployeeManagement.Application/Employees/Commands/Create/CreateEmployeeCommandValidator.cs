﻿using FluentValidation;

namespace EmployeeManagement.Application.Employees.Commands.Create;
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(10)
            .WithMessage("Name cannot be empty");

        // Email, Gender and PhoneNumber are already validated in value objects

        RuleFor(x => x.EmailAddress)
            .NotNull()
            .WithMessage("Email must be provided");

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .WithMessage("Phone number must be provided");

        RuleFor(x => x.Gender)
            .NotNull()
            .WithMessage("Gender must be provided");

        // But, if we have other properties that are not value objects we can validate them here
    }
}
