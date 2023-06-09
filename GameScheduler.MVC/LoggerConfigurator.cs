﻿using Serilog.Events;
using Serilog;

namespace GameScheduler.MVC
{
    public static class LoggerCongfigurator
    {
        public const string LogOutputTemplate = $"Exception: {{Exception}} {{Timestamp:HH:mm}} [l:{{Level}}] (th:{{ThreadId}}) Message: {{Message}}{{NewLine}}";

        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            var options = builder.Configuration.Get<LoggerOptions>();

            Enum.TryParse(options.LogLevel, true, out LogEventLevel logEventLevel);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: LogOutputTemplate)
                .WriteTo.File(options.LogFile, logEventLevel, LogOutputTemplate)
                .MinimumLevel.Is(logEventLevel)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }

    public class LoggerOptions
    {
        public string LogFile { get; set; }

        public string LogLevel { get; set; }
    }
}
