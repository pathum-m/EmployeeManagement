using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.Create;
public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateEmployeeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken) 
    {
        try
        {
            Result<Employee> employeeResult = Employee.Create(
                    command.Name,
                    command.EmailAddress,
                    command.PhoneNumber,
                    command.Gender);
            if (employeeResult.IsFailure)
            {
                _logger.LogError("Creating new employee has failed: {EmployeeName}", command.Name);
                return Result.Failure<string>(Error.EntityCreationFailure);
            }

            if (command.CafeId != null)
            {
                Result<Cafe> cafeResult = await _unitOfWork.Cafes.GetAsync(command.CafeId, cancellationToken);
                if (cafeResult.IsFailure)
                {
                    _logger.LogWarning("Cafe not found with ID: {CafeId}", command.CafeId.Value);
                    return Result.Failure<string>(DomainError.Cafe.NotFound);
                }
                cafeResult.Value.AddEmployee(employeeResult.Value);
                _unitOfWork.Cafes.UpdateAsync(cafeResult.Value, cancellationToken);
            }

            await _unitOfWork.Employees.AddAsync(employeeResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Employee created successfully ID: {EmployeeId}", employeeResult.Value.Id);
            return employeeResult.Value.Id.ToString();
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            _logger.LogError(ex, "Exception occurred when creating a employee : {ErrorMessage}", ex.Message);
            return Result.Failure<string>(new Error("500", ex.Message));
        }
    }
}
