namespace RealTimeWeatherMonitoringApp.Application.Interfaces;

/// <summary>
/// Defines a strategy for parsing a string into an instance of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TResult">The type of object to parse the string into.</typeparam>
public interface IParsingStrategy<TResult>
{
    /// <summary>
    /// Attempts to parse the provided string into an instance of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <param name="input">The string to attempt to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value of type <typeparamref name="TResult"/> if the parse operation was successful; otherwise, the default value for <typeparamref name="TResult"/>. This parameter is passed uninitialized.</param>
    /// <returns><c>true</c> if the string was successfully parsed; otherwise, <c>false</c>.</returns>
    bool TryParse(string input, out TResult? result);
}