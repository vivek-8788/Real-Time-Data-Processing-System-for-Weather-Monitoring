using System.Text.Json;
using System.Text.Json.Serialization;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Factory;

public class ConfigurationFactory : IConfigurationFactory
{
    private static readonly Lazy<JsonSerializerOptions> JsonOptions = new(() => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    });

    private readonly Lazy<IEnumerable<BotConfiguration>> _botConfigurations;

    public ConfigurationFactory(string configurationFilepath, IFileReader fileReader)
    {
        _botConfigurations = new Lazy<IEnumerable<BotConfiguration>>(() =>
        {
            var jsonText = fileReader.ReadAllText(configurationFilepath);
            var botDict = JsonSerializer.Deserialize<Dictionary<string, BotConfiguration>>(jsonText, JsonOptions.Value);
            return botDict == null
                ? Enumerable.Empty<BotConfiguration>()
                : botDict.Select(kvp => kvp.Value with { Name = kvp.Key });
        });
    }

    public IEnumerable<BotConfiguration> CreateBotConfigurations() => _botConfigurations.Value;
}