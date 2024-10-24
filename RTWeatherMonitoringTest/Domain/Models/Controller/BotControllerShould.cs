using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Domain.Models.Controller;
using RealTimeWeatherMonitoringTest.Domain.Common;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Domain.Models.Controller;

public class BotControllerShould
{
    private readonly Fixture _fixture;

    public BotControllerShould()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());
    }

    private BotEventArgs? CreateAndReact()
    {
        var inputEventArgs = _fixture.Create<DataChangeEventArgs<TestData>>();
        var botController = _fixture.Create<BotController<TestData>>();
        return botController.React(inputEventArgs);
    }

    [Fact]
    public void React_DataIsNull_IgnoreEvent()
    {
        _fixture.Inject<TestData?>(null);
        CreateAndReact().Should().BeNull();
    }

    [Fact]
    public void React_BotIsDisabled_IgnoreEvent()
    {
        _fixture.Customize(new BotCustomization<TestData>(enabled: false));
        CreateAndReact().Should().BeNull();
    }

    [Fact]
    public void React_EvaluateToFalse_IgnoreEvent()
    {
        _fixture.Customize(new FixedEvaluatorCustomization<TestData>(false));
        CreateAndReact().Should().BeNull();
    }

    [Fact]
    public void React_EvaluateToTrue_CreateEvent()
    {
        _fixture.Customize(new BotCustomization<TestData>(enabled: true));
        _fixture.Customize(new FixedEvaluatorCustomization<TestData>(true));
        var bot = _fixture.Freeze<Bot<TestData>>();

        var outputEventArgs = CreateAndReact();

        outputEventArgs.Should().NotBeNull();
        outputEventArgs.BotName.Should().Be(bot.Name);
        outputEventArgs.Message.Should().Be(bot.Message);
    }
}