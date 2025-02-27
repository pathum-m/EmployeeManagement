using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.Delete;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<bool>>
{
    private readonly IEmployeeRespository _employeeRepository;
    private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

    public DeleteEmployeeCommandHandler(IEmployeeRespository employeeRepository,
            ILogger<DeleteEmployeeCommandHandler> logger)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken) 
    {
        Result<Employee> employeeResult = await _employeeRepository.GetAsync(command.Id, cancellationToken);

        if (employeeResult.IsFailure)
        {
            _logger.LogWarning("Employee could not find ID: {EmployeeId}", command.Id);
            return false;
        }

         _employeeRepository.DeleteAsync(employeeResult.Value);

        _logger.LogInformation("Employee {EmployeeId} deleted successfully", command.Id);

        return true;
    }
}
