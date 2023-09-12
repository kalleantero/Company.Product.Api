using Company.Product.Application.Models.Request;
using Company.Product.Application.Models.Response;
using FluentResults;

namespace Company.Product.Application.Interfaces;

public interface IExampleService
{
    Task<Result<ExampleResponseModel>> GetExampleByIdASync(Guid exampleId, CancellationToken cancellationToken);
    Task<Result> CreateNewAsync(NewExampleRequestModel newExampleRequestModel, CancellationToken cancellationToken);
}
