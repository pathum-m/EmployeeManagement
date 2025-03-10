using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints : CarterModule
{
    public CafeEndpoints() : base("api/cafes") => WithTags("Cafe"); // for Swagger

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", GetCafesEndpoint)
            .WithSummary("List of cafes")
            .WithDescription("A list of cafes of a given location. If locatoin is invalid return empty list. If location not provided returns all the cafes");

        app.MapPost("", CafePostEndpoint)
           .AllowAnonymous();

        app.MapPatch("{id:guid}", CafePatchEndpoint)
          .AllowAnonymous();

        app.MapDelete("{id:guid}", CafeDeleteEndpoint)
          .AllowAnonymous();
    }
}
