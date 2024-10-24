using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Presentation.Controller;

public class ServerInterfaceController
{
    private readonly IDataChangeNotifier<WeatherData> _weatherDataNotifier;
    private readonly IBotPublishingService _botPublishingService;
    private readonly IBotEventManager<WeatherData> _weatherDataBotEventManager;

    public ServerInterfaceController(
        IDataChangeNotifier<WeatherData> weatherDataNotifier,
        IBotPublishingService botPublishingService,
        IBotEventManager<WeatherData> weatherDataBotEventManager)
    {
        _weatherDataNotifier = weatherDataNotifier;
        _botPublishingService = botPublishingService;
        _weatherDataBotEventManager = weatherDataBotEventManager;
    }

    public void Start()
    {
        _weatherDataBotEventManager.Attach(_weatherDataNotifier, _botPublishingService);
        Console.WriteLine("Server:\tBots are now listening for weather data...");
        Console.WriteLine("Server:\t[Tip] Use weather data processor to send data to the system.");
        Console.WriteLine("Server:\t[Tip] Subscribe to bot notifications to receive any new events.");
        Console.WriteLine(new string('-', 70));
    }
}