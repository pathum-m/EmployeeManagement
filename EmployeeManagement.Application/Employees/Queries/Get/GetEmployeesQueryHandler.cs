using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Queries.Get;
public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, Result<List<EmployeeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetEmployeesQueryHandler> _logger;

    public GetEmployeesQueryHandler(IUnitOfWork unitOfWork, ILogger<GetEmployeesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<EmployeeDto>>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            Result<IEnumerable<EmployeeDto>> employeesResult = await _unitOfWork.Employees.GetEmployeesWithCafeNameAsync(query.CafeId,
            (employee, cafe) => new EmployeeDto
            (
                employee.Id.Value,
                employee.Name,
                employee.EmailAddress.Value,
                employee.PhoneNumber.Value,
                employee.Gender.ToString(),
                employee.CalculateDaysWorked(),
                cafe
            ),
            cancellationToken);

            if (employeesResult.IsFailure)
            {
                _logger.LogError("Fetching employees request has failed: Params {Query}", query);
                return Result.Failure<List<EmployeeDto>>(Error.NotFound);
            }

            var employeeDtos = employeesResult.Value
                .OrderByDescending(e => e.DaysWorked)
                .ToList();

            _logger.LogInformation("Retrieved {Count} employees", employeeDtos.Count);
            return employeeDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred when retrieving employees : {ErrorMessage}", ex.Message);
            return Result.Failure<List<EmployeeDto>>(new Error("500", ex.Message));
        }
    }
}
