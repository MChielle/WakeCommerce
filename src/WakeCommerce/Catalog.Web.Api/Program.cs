using Catalog.Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.ConfigureRewriter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
