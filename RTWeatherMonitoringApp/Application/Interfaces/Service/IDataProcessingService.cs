using RealTimeWeatherMonitoringApp.Application.Common;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

/// <summary>
/// Responsible for passing processed data to the system
/// </summary>
/// <typeparam name="TData">The data to be processed and passed</typeparam>
public interface IDataProcessingService<TData>
{
    /// <summary>
    /// Parses the provided input string to extract data and passes the result through the system.
    /// </summary>
    /// <param name="input">The raw input string containing data to be parsed.</param>
    /// <returns>A <see cref="ParsingResult{TData}"/> object that encapsulates the result of the parsing operation,
    /// indicating success or failure, and containing the parsed <see cref="TData"/> if successful.</returns>
    ParsingResult<TData> Process(string input);

    /// <summary>
    /// Asynchronous version of <see cref="Process"/>
    /// </summary>
    Task<ParsingResult<TData>> ProcessAsync(string input);
}