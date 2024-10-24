using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Presentation.Controller;

public class UserInterfaceController
{
    private readonly IBotNotificationService _botNotificationService;
    private readonly IDataProcessingService<WeatherData> _weatherDataProcessor;

    public UserInterfaceController(
        IBotNotificationService botNotificationService,
        IDataProcessingService<WeatherData> weatherDataProcessor)
    {
        _botNotificationService = botNotificationService;
        _weatherDataProcessor = weatherDataProcessor;
    }

    public async Task Start()
    {
        _botNotificationService.OnBotNotification += (_, args) =>
            Console.WriteLine($"\n{args.BotName}:  {args.Message}");

        Console.WriteLine("User:\tSubscribed to bot notifications.");
        Console.WriteLine("User:\tYou can now enter weather data to process.");
        Console.WriteLine("User:\t[Tip] No need to specify data format, the system auto detects it.");
        Console.WriteLine("User:\t[Tip] Enter (q) to quit.");

        while (true)
        {
            Console.WriteLine(new string('-', 70));
            Console.WriteLine("User:  Enter weather data to process.");

            var input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase)) break;

            var result = await _weatherDataProcessor.ProcessAsync(input);
            if (result.Fail) Console.WriteLine(result.Message);
        }
    }
}