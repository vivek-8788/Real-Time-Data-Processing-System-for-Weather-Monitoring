using AutoFixture;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringTest.Domain.Common;

public class BotCustomization<TEvaluated> : ICustomization
{
    private readonly bool _enabled;
    private readonly string? _name;
    private readonly string? _message;
    private readonly IEvaluator<TEvaluated>? _evaluator;

    public BotCustomization(
        bool enabled,
        string? name = null,
        string? message = null,
        IEvaluator<TEvaluated>? evaluator = null)
    {
        _enabled = enabled;
        _name = name;
        _message = message;
        _evaluator = evaluator;
    }

    public void Customize(IFixture fixture)
    {
        fixture.Customize<Bot<TEvaluated>>(composer => composer.FromFactory(() =>
        {
            var bot = new Bot<TEvaluated>(
                _name ?? fixture.Create<string>(),
                _enabled,
                _message ?? fixture.Create<string>(),
                _evaluator ?? fixture.Create<IEvaluator<TEvaluated>>());

            return bot;
        }));
    }
}