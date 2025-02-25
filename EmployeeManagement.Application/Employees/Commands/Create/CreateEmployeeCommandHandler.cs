using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.Create;
internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<string>>
{
    private readonly IEmployeeRespository _employeeRepository;
    private readonly ICafeRepository _cafeRepository;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;

    public CreateEmployeeCommandHandler(
        IEmployeeRespository employeeRepository,
        ICafeRepository cafeRepository,
        ILogger<CreateEmployeeCommandHandler> logger)
    {
        _employeeRepository = employeeRepository;
        _cafeRepository = cafeRepository;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken) 
    {
        Result<Email> emailResult = Email.Create(command.EmailAddress);
        if (emailResult.IsFailure)
        {
            return Result.Failure<string>(DomainError.Employee.InvalidEmail);
        }

        Result<PhoneNumber> phoneResult = PhoneNumber.Create(command.PhoneNumber);
        if (phoneResult.IsFailure)
        {
            return Result.Failure<string>(DomainError.Employee.InvalidPhoneNumber);
        }

        Result<Employee> employeeResult = Employee.Create(
                command.Name,
                emailResult.Value,
                phoneResult.Value,
                command.Gender);

        if (employeeResult.IsFailure)
        {
            _logger.LogError("Creating new employee has failed: {EmployeeName}", command.Name);
            return Result.Failure<string>(Error.EntityCreationFailure);
        }

        if (command.CafeId != null)
        {
            Result<Cafe> cafeResult = await _cafeRepository.GetAsync(command.CafeId, cancellationToken);
            if (cafeResult == null)
            {
                _logger.LogWarning("Cafe not found with ID: {CafeId}", command.CafeId.Value);
                return Result.Failure<string>(DomainError.Cafe.NotFound);
            }

            cafeResult.Value.AddEmployee(employeeResult.Value, DateTime.UtcNow);
        }

        await _employeeRepository.AddAsync(employeeResult.Value, cancellationToken);

        _logger.LogInformation("Employee created successfully ID: {EmployeeId}", employeeResult.Value.Id);

        return employeeResult.Value.Id.ToString();
    }
}
