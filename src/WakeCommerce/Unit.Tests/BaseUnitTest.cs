using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shared.Defaults.Providers;

namespace Unit.Tests
{
    public abstract class BaseUnitTest
    {
        public readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
        public readonly DbContextOptions<ApplicationDbContext> _dbOptions;

        protected BaseUnitTest()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(new DateTime(2025, 01, 01));

            _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // fresh DB for each test
                .Options;
        }
    }
}