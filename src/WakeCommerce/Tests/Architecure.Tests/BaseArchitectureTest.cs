using Catalog.Application.Abstractions.Requests;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using System.Reflection;

namespace Architecture.Tests
{
    public abstract class BaseArchitectureTest
    {
        protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
        protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
        protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
        protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
    }
}