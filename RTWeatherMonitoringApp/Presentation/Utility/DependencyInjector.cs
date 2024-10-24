using Microsoft.Extensions.DependencyInjection;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Repository;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Domain.Service;
using RealTimeWeatherMonitoringApp.Infrastructure.Factory;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;
using RealTimeWeatherMonitoringApp.Infrastructure.Repository;
using RealTimeWeatherMonitoringApp.Infrastructure.Service;
using RealTimeWeatherMonitoringApp.Presentation.Controller;

namespace RealTimeWeatherMonitoringApp.Presentation.Utility;

public static class DependencyInjector
{
    public static IServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        InjectInfrastructure(services);
        InjectDomain(services);
        InjectApplication(services);
        InjectPresentation(services);
        return services.BuildServiceProvider();
    }

    private static void InjectInfrastructure(IServiceCollection services)
    {
        services.AddSingleton<IFileReader>(_ => new RelativeFileReader(new FileReader()));
        services.AddSingleton<IConfigurationFactory, ConfigurationFactory>(p =>
        {
            var fileReader = p.GetRequiredService<IFileReader>();
            return new ConfigurationFactory(DirectoryStructureUtility.ConfigurationFilepath, fileReader);
        });

        services.AddSingleton<IEvaluatorFactory<WeatherData>, WeatherEvaluatorFactory>();
        services.AddSingleton<IBotFactory<WeatherData>, BotFactory<WeatherData>>();
        services.AddSingleton<IBotInitializer<WeatherData>, BotInitializer<WeatherData>>();
    }

    private static void InjectDomain(IServiceCollection services)
    {
        services.AddSingleton<IBotRepository<WeatherData>, BotRepository<WeatherData>>();
        services.AddSingleton<IBotControllerService<WeatherData>, BotControllerService<WeatherData>>();
    }


    private static void InjectApplication(IServiceCollection services)
    {
        services.AddSingleton<IAutoParsingService<WeatherData>>(_ =>
        {
            var service = new AutoParsingService<WeatherData>();
            foreach (var parserType in
                     DirectoryStructureUtility.GetAllTypes(DirectoryStructureUtility.ParsersNamespace))
            {
                if (Activator.CreateInstance(parserType) is IParsingStrategy<WeatherData> parser)
                    service.AddStrategy(parser);
            }

            return service;
        });

        services.AddSingleton<MonitoringService<WeatherData>>();
        services.AddSingleton<IDataReceiver<WeatherData>>(
            p => p.GetRequiredService<MonitoringService<WeatherData>>());
        services.AddSingleton<IDataChangeNotifier<WeatherData>>(
            p => p.GetRequiredService<MonitoringService<WeatherData>>());

        services.AddSingleton<BotEventDispatcher>();
        services.AddSingleton<IBotPublishingService>(p => p.GetRequiredService<BotEventDispatcher>());
        services.AddSingleton<IBotNotificationService>(p => p.GetRequiredService<BotEventDispatcher>());

        services.AddSingleton<IBotEventManager<WeatherData>, BotEventManager<WeatherData>>();
        services.AddSingleton<IDataProcessingService<WeatherData>, DataProcessingService<WeatherData>>();
    }

    private static void InjectPresentation(IServiceCollection services)
    {
        services.AddSingleton<UserInterfaceController>();
        services.AddSingleton<ServerInterfaceController>();
    }
}