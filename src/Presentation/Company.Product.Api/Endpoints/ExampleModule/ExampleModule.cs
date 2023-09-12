using Company.Product.Application.Models.Response;
using Company.Product.Application.Models.Request;
using Company.Product.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Product.Api.Endpoints.ExampleModule;

public static class ExampleModule
{
    public static void RegisterExampleModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/example", GetExampleById)
            .WithName("GetExampleById")
            .WithDisplayName("Get Example by Id")
            .WithTags("ExampleModule")
            .Produces<ExampleResponseModel>()
            .Produces(500);

        endpoints.MapPost("/example", CreateNew)
            .WithName("CreateNew")
            .WithDisplayName("Create a new example")
            .WithTags("ExampleModule")
            .Produces(500);
    }

    private static async Task<IResult> GetExampleById([FromServices] IExampleService exampleService, Guid exampleId, CancellationToken cancellationToken)
    {
        var example = await exampleService.GetExampleByIdASync(exampleId, cancellationToken);
        return Results.Ok(example);
    }

    private static async Task<IResult> CreateNew([FromServices] IExampleService exampleService, NewExampleRequestModel exampleRequestModel, CancellationToken cancellationToken)
    {
        var example = await exampleService.CreateNewAsync(exampleRequestModel, cancellationToken);
        return Results.Ok(example);
    }
}