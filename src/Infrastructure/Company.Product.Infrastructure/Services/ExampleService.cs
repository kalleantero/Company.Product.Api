using Bogus;
using Company.Product.Application.Interfaces;
using Company.Product.Application.Models.Request;
using Company.Product.Application.Models.Response;
using FluentResults;

namespace Company.Product.Infrastructure.Services;

internal sealed class ExampleService : IExampleService
{

    public async Task<Result<ExampleResponseModel>> GetExampleByIdASync(Guid exampleId, CancellationToken cancellationToken)
    {
        //Guard.Against.Null(exampleRequestModel, nameof(ExampleRequestModel));

        var fakeData = new Faker<ExampleResponseModel>()
            .RuleFor(u => u.Id, f => exampleId)
            .RuleFor(u => u.Name, f => f.Lorem.Word())
            .RuleFor(u => u.Description, f => f.Lorem.Text());

        var response = fakeData.Generate();

        if (response == null)
        {
            return Result.Fail("Example generation failed.");
        }

        return response;
    }

    public async Task<Result> CreateNewAsync(NewExampleRequestModel newExampleRequestModel, CancellationToken cancellationToken)
    {
        if (newExampleRequestModel == null)
        {
            return Result.Fail("Example request cannot be null.");
        }

        return Result.Ok();
    }
}
