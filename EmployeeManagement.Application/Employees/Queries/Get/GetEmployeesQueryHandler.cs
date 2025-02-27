using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Queries.Get;
public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, Result<List<EmployeeDto>>>
{
    private readonly IEmployeeRespository _employeeRepository;
    private readonly ICafeRepository _cafeRepository;
    private readonly ILogger<GetEmployeesQueryHandler> _logger;

    public GetEmployeesQueryHandler(IEmployeeRespository employeeRepository,
            ICafeRepository cafeRepository,
            ILogger<GetEmployeesQueryHandler> logger)
    {
        _employeeRepository = employeeRepository;
        _cafeRepository = cafeRepository;
        _logger = logger;
    }

    public async Task<Result<List<EmployeeDto>>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
    {
        Result<IEnumerable<Employee>> employeesResult = query.CafeId.HasValue
                ? await _employeeRepository.GetByCafeIdAsync(query.CafeId.Value, cancellationToken)
                : await _employeeRepository.GetAllAsync(cancellationToken);

        if (employeesResult.IsFailure)
        {
            _logger.LogError("Fetching employees request has failed: Params {Query}", query);
            return Result.Failure<List<EmployeeDto>>(Error.NotFound);
        }

        var employeeDtos = employeesResult.Value
            .Select(e => new EmployeeDto
            (
                e.Id.Value,
                e.Name,
                e.EmailAddress.Value,
                e.PhoneNumber.Value,
                e.Gender.ToString(),
                e.CalculateDaysWorked()
            ))
            .OrderByDescending(e => e.DaysWorked)
            .ToList();

        _logger.LogInformation("Retrieved {Count} employees", employeeDtos.Count);

        return employeeDtos;
    }
}
