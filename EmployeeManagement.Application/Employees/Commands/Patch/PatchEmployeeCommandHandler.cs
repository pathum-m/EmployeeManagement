using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.Patch;
public sealed class PatchEmployeeCommandHandler : IRequestHandler<PatchEmployeeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PatchEmployeeCommandHandler> _logger;

    public PatchEmployeeCommandHandler(IUnitOfWork unitOfWork, ILogger<PatchEmployeeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(PatchEmployeeCommand command, CancellationToken cancellationToken)
    {
        try
        {
            Result<Employee> employeeResult = await _unitOfWork.Employees.GetAsync(command.Id, cancellationToken);
            if (employeeResult.IsFailure)
            {
                _logger.LogWarning("Could not find the Employee ID: {EmployeeId}", command.Id.Value);
                return false;
            }

            Employee employee = employeeResult.Value;
            Result<bool> updateResult = employee.UpdateDetails(command.Name, command.EmailAddress, command.PhoneNumber, command.Gender);
            if (updateResult.IsFailure)
            {
                _logger.LogError("Updating employee {EmployeeName} has failed: ", command.Name);
                return Result.Failure<bool>(updateResult.Error);
            }

            if (command.CafeId != null)
            {
                Result<Cafe> cafeResult = await _unitOfWork.Cafes.GetAsync(command.CafeId, cancellationToken);
                if (cafeResult.IsFailure)
                {
                    _logger.LogWarning("Cafe not found with ID: {CafeId}", command.CafeId.Value);
                    return Result.Failure<bool>(DomainError.Cafe.NotFound);
                }
                cafeResult.Value.AddEmployee(employeeResult.Value);
                _unitOfWork.Cafes.UpdateAsync(cafeResult.Value, cancellationToken);
            }

            _unitOfWork.Employees.UpdateAsync(employee, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            _logger.LogCritical(ex, "Exception updating employee with ID: {EmployeeId}", command.Id.Value);
            return Result.Failure<bool>(new Error("500", ex.Message));
        }
    }
}
