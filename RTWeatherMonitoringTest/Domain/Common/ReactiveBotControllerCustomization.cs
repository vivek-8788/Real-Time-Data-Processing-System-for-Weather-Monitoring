using AutoFixture;

namespace RealTimeWeatherMonitoringTest.Domain.Common;

/// <summary>
/// Ensures that generated BotControllers will always
/// react to DataChangeEventArgs (Except when data is null).
/// </summary>
public class ReactiveBotControllerCustomization<TEvaluated> : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize(new BotCustomization<TEvaluated>(enabled: true));
        fixture.Customize(new FixedEvaluatorCustomization<TEvaluated>(true));
    }
}