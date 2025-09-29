namespace Architecture.Tests.Layers
{
    public class LayerDepencencyTests : BaseArchitectureTest
    {
        [Fact]
        public void Domain_Should_Not_Depend_On_Any_Other_Layer()
        {
            var result = DomainAssembly
                .GetReferencedAssemblies()
                .Where(x =>
                    x.Name == ApplicationAssembly.GetName().Name
                    ||
                    x.Name == InfrastructureAssembly.GetName().Name
                    ||
                    x.Name == PresentationAssembly.GetName().Name
                );

            Assert.Empty(result);
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure_Or_Presentation()
        {
            var result = ApplicationAssembly
                .GetReferencedAssemblies()
                .Where(x =>
                    x.Name == DomainAssembly.GetName().Name
                    ||
                    x.Name == InfrastructureAssembly.GetName().Name
                    ||
                    x.Name == PresentationAssembly.GetName().Name
                );
            Assert.True(result.Count() == 1);
            Assert.Matches(result.FirstOrDefault()?.Name ?? "", DomainAssembly.GetName().Name);
        }

        [Fact]
        public void Infrastructure_Should_Not_Depend_On_Presentation_Or_Domain()
        {
            var result = InfrastructureAssembly
                .GetReferencedAssemblies()
                .Where(x =>
                    x.Name == DomainAssembly.GetName().Name
                    ||
                    x.Name == ApplicationAssembly.GetName().Name
                    ||
                    x.Name == PresentationAssembly.GetName().Name
                );

            Assert.True(result.Count() == 2);
            Assert.True(result?.Any(x => x.Name == DomainAssembly.GetName().Name) ?? false);
            Assert.True(result?.Any(x => x.Name == ApplicationAssembly.GetName().Name) ?? false);
        }

        [Fact]
        public void Presentation_Should_Not_Depend_On_Application_Or_Domain()
        {
            var result = PresentationAssembly
                .GetReferencedAssemblies()
                .Where(x =>
                    x.Name == DomainAssembly.GetName().Name
                    ||
                    x.Name == ApplicationAssembly.GetName().Name
                    ||
                    x.Name == InfrastructureAssembly.GetName().Name
                );

            Assert.True(result.Count() == 2);
            Assert.True(result?.Any(x => x.Name == InfrastructureAssembly.GetName().Name) ?? false);
            Assert.True(result?.Any(x => x.Name == ApplicationAssembly.GetName().Name) ?? false);
        }
    }
}