﻿using System.Linq.Expressions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Domain.Abstractions.Repositories;

public interface IEmployeeRespository : IRepository<Employee, EmployeeId>
{
    Task<Result<IEnumerable<Employee>>> GetByCafeIdAsync(CafeId cafeId, CancellationToken cancellationToken = default);
    Task<Result<bool>> IsEmployeeAssignedToCafeAsync(EmployeeId employeeId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TProjection>>> GetEmployeesWithCafeNameAsync<TProjection>(CafeId? cafeId, Expression<Func<Employee, string, TProjection>> projection, CancellationToken cancellationToken = default);
}
