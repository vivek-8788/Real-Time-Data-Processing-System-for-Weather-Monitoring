using RealTimeWeatherMonitoringApp.Application.Common;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

namespace RealTimeWeatherMonitoringApp.Application.Service;

/// <summary>
/// A simple iterative approach,
/// tries all strategies one by one until it finds a valid parsing strategy and returns the result.
/// </summary>
/// <typeparam name="TResult">Parsed data type</typeparam>
public class AutoParsingService<TResult> : IAutoParsingService<TResult>
{
    private readonly List<IParsingStrategy<TResult>> _strategies = [];
    public void AddStrategy(IParsingStrategy<TResult> strategy) => _strategies.Add(strategy);

    public ParsingResult<TResult> AutoParse(string input)
    {
        foreach (var strategy in _strategies)
            if (strategy.TryParse(input, out var result))
                return new ParsingResult<TResult>(true, $"Parsing succeeded with strategy '{strategy}'")
                    { Data = result };

        return new ParsingResult<TResult>(false, $"No valid parsing strategy was found for input '{input}'");
    }

    public async Task<ParsingResult<TResult>> AutoParseAsync(string input) =>
        await Task.Run(() => AutoParse(input));
}