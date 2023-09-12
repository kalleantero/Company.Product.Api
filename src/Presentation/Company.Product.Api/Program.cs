using Company.Product.Api.Extensions;
using Company.Product.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonServices();

builder.Services.AddInfrastructure();

var app = builder.Build();

app.ConfigureApplication();

app.Run();