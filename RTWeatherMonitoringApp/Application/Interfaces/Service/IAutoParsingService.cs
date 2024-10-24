using RealTimeWeatherMonitoringApp.Application.Common;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

/// <summary>
/// Automatically selects and applies the appropriate parsing strategy
/// to convert a string into an instance of type <typeparamref name="TResult"/>.
/// The selection of the parsing strategy is based on the format or content of the input string.
/// </summary>
/// <typeparam name="TResult">The result type into which the string will be parsed.</typeparam>
public interface IAutoParsingService<TResult>
{
    /// <summary>
    /// Attempts to automatically detect the appropriate parsing strategy based on the input string and
    /// parses it into an instance of type <typeparamref name="TResult"/>. The selection of the parsing strategy
    /// is based on the format or content of the input string.
    /// </summary>
    /// <param name="input">The string to attempt to parse, whose format or content dictates the choice of parsing strategy.</param>
    /// <returns>A <see cref="ParsingResult{TResult}"/> object that encapsulates the result of the parsing operation,
    /// indicating success or failure, and containing the parsed <typeparamref name="TResult"/> if successful.</returns>
    ParsingResult<TResult> AutoParse(string input);

    /// <summary>
    /// Asynchronous version of <see cref="AutoParse"/>
    /// </summary>
    Task<ParsingResult<TResult>> AutoParseAsync(string input);
}