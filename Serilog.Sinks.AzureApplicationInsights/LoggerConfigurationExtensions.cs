using System;
using Serilog.Configuration;
using Serilog.Events;

namespace Serilog.Sinks.AzureApplicationInsights
{
    /// <summary>
    /// Adds the WriteTo.AzureApplicationInsights() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events against Microsoft Application Insights for the provided component id.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="applicationInsightsInstrumentationKey">The Instrumentation Key provided by Application Insights.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">loggerConfiguration</exception>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        [Obsolete("This Sink is no longer supported - I moved its functionality over to the core 'Serilog.Sinks.ApplicationInsights' NuGet package. Use that one instead", false)]
        public static LoggerConfiguration AzureApplicationInsights(
            this LoggerSinkConfiguration loggerConfiguration,
            string applicationInsightsInstrumentationKey,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");

            if (applicationInsightsInstrumentationKey == null) throw new ArgumentNullException("applicationInsightsInstrumentationKey");
            if (string.IsNullOrWhiteSpace(applicationInsightsInstrumentationKey)) throw new ArgumentOutOfRangeException("applicationInsightsInstrumentationKey", "Cannot be empty.");

            return loggerConfiguration.Sink(
                new ApplicationInsightsSink(applicationInsightsInstrumentationKey, formatProvider),
                restrictedToMinimumLevel);
        }
    }
}
