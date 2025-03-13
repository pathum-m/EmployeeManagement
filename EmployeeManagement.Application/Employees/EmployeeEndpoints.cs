using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EmployeeManagement.Application.Employees;
public partial class EmployeeEndpoints : CarterModule
{
    public EmployeeEndpoints() : base("api/employees") => WithTags("Employee"); // for Swagger

    public override void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("", GetEmployeesByCafeEndpoint)
            .WithSummary("List of employees of a cafe")
            .WithDescription("List of employees of a cafe")
            .AllowAnonymous();

        app.MapPost("", EmployeePostEndpoint)
           .AllowAnonymous();

        app.MapPatch("{id}", EmployeePatchEndpoint)
          .AllowAnonymous();

        app.MapDelete("{id}", EmployeeDeleteEndpoint)
          .AllowAnonymous();
    }
}
