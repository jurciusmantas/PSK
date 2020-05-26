using System;

namespace PSK.Model.Logging
{
    public enum LogLevel
    {
        Debug,
        Verb,
        Info,
        Warn,
        Err
    }

    public class LogLevelHelper
    {
        public static LogLevel FromString(string value)
        {
            return value switch
            {
                "Debug" => LogLevel.Debug,
                "Verb" => LogLevel.Verb,
                "Info" => LogLevel.Info,
                "Warn" => LogLevel.Warn,
                "Err" => LogLevel.Err,
                _ => throw new ArgumentException($"'{value}' is not a valid log level."),
            };
        }
    }
}
