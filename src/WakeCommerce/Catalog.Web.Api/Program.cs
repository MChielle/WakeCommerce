using Catalog.Application;
using Catalog.Infrastructure;
using Catalog.Web.Api;
using Catalog.Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.ConfigureRewriter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
