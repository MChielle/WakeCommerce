using AslaveCare.IntegrationTests.Configuration;
using Catalog.Infrastructure.Database;
using Catalog.Infrastructure.Database.Seeders;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>
    {
        protected HttpClient _client;
        private IServiceScope _scope;

        protected BaseIntegrationTest(IntegrationTestWebApplicationFactory factory)
        {
            _scope = factory.Services.CreateScope();

            var context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreatedAsync();

            _client = factory.CreateClient();
        }
    }
}