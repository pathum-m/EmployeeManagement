using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.Delete;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteEmployeeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken) 
    {
        try
        {
            Result<Employee> employeeResult = await _unitOfWork.Employees.GetAsync(command.Id, cancellationToken);
            if (employeeResult.IsFailure)
            {
                _logger.LogWarning("Employee could not find ID: {EmployeeId}", command.Id);
                return false;
            }

            _unitOfWork.Employees.DeleteAsync(employeeResult.Value);
            _logger.LogInformation("Employee {EmployeeId} deleted successfully", command.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred when deleting a employee : {ErrorMessage}", ex.Message);
            return Result.Failure<bool>(new Error("500", ex.Message));
        }
    }
}
