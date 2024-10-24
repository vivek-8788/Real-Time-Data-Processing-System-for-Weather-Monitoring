using System.Reflection;
using FluentAssertions;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Presentation.Utility;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Presentation;

public class DirectoryStructureShould
{
    [Fact]
    public void IParsingStrategyImplementations_ResideInParsersDirectory()
    {
        foreach (var type in GetImplementationsOfGenericInterface(typeof(IParsingStrategy<>)))
        {
            type.Namespace.Should().NotBeNull()
                .And.StartWith(DirectoryStructureUtility.ParsersNamespace,
                    "because all IParsingStrategy implementations should reside in the Parsers directory");
        }
    }

    private static IEnumerable<Type> GetImplementationsOfGenericInterface(Type genericInterfaceType)
    {
        return Assembly.GetAssembly(genericInterfaceType)!.GetTypes()
            .Where(t => !t.IsAbstract && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType));
    }
}