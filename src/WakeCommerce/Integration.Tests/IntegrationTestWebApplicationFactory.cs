using Catalog.Application.Abstractions.Data;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Integration.Tests
{
    public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (dbContextDescriptor is not null) services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options
                    .UseInMemoryDatabase("inMemoryDatabase");
                });

                //services.AddEntityFrameworkSqlite()
                //    .AddDbContext<ApplicationDbContext>(options =>
                //    {
                //        options.UseSqlite("Data Source=database.dat");

                //        options.UseInternalServiceProvider(services.BuildServiceProvider());
                //    });

                services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

                //services.ConfigureDbContext<ApplicationDbContext>(options =>
                //{
                //    options.UseSeeding(async (context, _) =>
                //    {
                //        await TestSeeder.SeedAsync(context);
                //    });
                //});
            });

            builder.UseEnvironment("Test");
        }

        protected override void Dispose(bool disposing)
        {
            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureDeleted();
                //See about base Dispose: https://learn.microsoft.com/en-us/dotnet/api/system.idisposable.dispose?view=net-8.0
            }
        }
    }
}