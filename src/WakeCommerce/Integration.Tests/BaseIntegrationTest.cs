using Catalog.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Tests
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