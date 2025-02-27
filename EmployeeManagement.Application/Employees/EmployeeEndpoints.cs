using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EmployeeManagement.Application.Employees;
public partial class EmployeeEndpoints : CarterModule
{
    public EmployeeEndpoints() : base("api/employees") => WithTags("Cafe"); // for Swagger

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        //app.MapGet("{location}", GetCafesEndpoint)
        //    .WithSummary("List of employees for a cafe")
        //    .WithDescription("A list of employees of a given cafe");
    }
}
