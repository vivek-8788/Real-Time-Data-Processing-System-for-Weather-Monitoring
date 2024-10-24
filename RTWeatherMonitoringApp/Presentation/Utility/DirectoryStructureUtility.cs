using System.Reflection;

namespace RealTimeWeatherMonitoringApp.Presentation.Utility;

public static class DirectoryStructureUtility
{
    /// <summary>
    /// Where all implementations of IParsingStrategy should reside.
    /// </summary>
    public const string ParsersNamespace = "RealTimeWeatherMonitoringApp.Infrastructure.Parsers";

    /// <summary>
    /// Where configuration json file resides.
    /// </summary>
    public const string ConfigurationFilepath = "Infrastructure/Configuration/configuration.json";

    /// <summary>
    /// Retrieves all types within a specified namespace.
    /// </summary>
    /// <param name="namespace">The namespace to search for types.</param>
    /// <returns>An enumerable of types within the specified namespace.</returns>
    public static IEnumerable<Type> GetAllTypes(string @namespace)
    {
        if (string.IsNullOrEmpty(@namespace))
            throw new ArgumentException("Namespace cannot be null or empty.", nameof(@namespace));

        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(t => string.Equals(t.Namespace, @namespace, StringComparison.Ordinal))
            .ToList();

        return types;
    }
}