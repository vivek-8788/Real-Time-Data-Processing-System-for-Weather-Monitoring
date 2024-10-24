using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;

namespace RealTimeWeatherMonitoringApp.Domain.Models.Controller;

public class BotController<TEvaluated> : IBotController<TEvaluated>
{
    private readonly Bot<TEvaluated> _bot;
    public BotController(Bot<TEvaluated> bot) => _bot = bot;

    public BotEventArgs? React(DataChangeEventArgs<TEvaluated> args) =>
        args.NewData != null && IsTriggeredBy(args.NewData) ? new BotEventArgs(_bot.Name, _bot.Message) : null;

    private bool IsTriggeredBy(TEvaluated data) => _bot.Enabled && _bot.Evaluator.Evaluate(data);
}