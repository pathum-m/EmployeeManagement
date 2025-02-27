using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints : CarterModule
{
    public CafeEndpoints() : base("api") => WithTags("Cafe"); // for Swagger

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("cafes/{location:string}", GetCafesEndpoint)
            .WithSummary("List of cafes")
            .WithDescription("A list of cafes of a given location. If locatoin is invalid return empty list. If location not provided returns all the cafes");

        app.MapPost("cafe", CafePostEndpoint)
           .AllowAnonymous();

        //app.MapPatch("current", CafeUpdateEndpoint)
        //  .AllowAnonymous();

        //app.MapPost("delete", CafeDeleteEndpoint)
        //  .AllowAnonymous();
    }
}
