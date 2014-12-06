using System;
using System.Globalization;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.AzureApplicationInsights
{
    /// <summary>
    /// Writes log events to a Microsoft Azure Application Insights account.
    /// Inspired by their NLog Appender implementation.
    /// </summary>
    public class ApplicationInsightsSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        /// <summary>
        /// Holds the actual Application Insights TelemetryClient that will be used for logging.
        /// </summary>
        private readonly TelemetryClient _telemetryClient;
        
        /// <summary>
        /// Construct a sink that saves logs to the specified storage account.
        /// </summary>
        /// <param name="applicationInsightsInstrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public ApplicationInsightsSink(string applicationInsightsInstrumentationKey, IFormatProvider formatProvider)
        {
            if (applicationInsightsInstrumentationKey == null) throw new ArgumentNullException("applicationInsightsInstrumentationKey");
            if (string.IsNullOrWhiteSpace(applicationInsightsInstrumentationKey)) throw new ArgumentOutOfRangeException("applicationInsightsInstrumentationKey", "Cannot be empty.");

            _formatProvider = formatProvider;
            _telemetryClient = new TelemetryClient();

            if (!string.IsNullOrWhiteSpace(applicationInsightsInstrumentationKey))
                _telemetryClient.Context.InstrumentationKey = applicationInsightsInstrumentationKey;
        }
        
        #region Implementation of ILogEventSink

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            // writing logEvent as TraceTelemetry properties
            var traceTelemetry = new TraceTelemetry(logEvent.RenderMessage(_formatProvider));

            // and forwaring properties and logEvent Data to the traceTelemetry's properties
            var properties = traceTelemetry.Context.Properties;
            properties.Add("Level", logEvent.Level.ToString());
            properties.Add("TimeStamp", logEvent.Timestamp.ToString(CultureInfo.InvariantCulture));
            properties.Add("MessageTemplate", logEvent.MessageTemplate.Text);

            if (logEvent.Exception != null)
            {
                properties.Add("Exception", logEvent.Exception.Message);

                if (string.IsNullOrWhiteSpace(logEvent.Exception.Source) == false)
                    properties.Add("ExceptionSource", logEvent.Exception.Source);

                if (string.IsNullOrWhiteSpace(logEvent.Exception.StackTrace) == false)
                    properties.Add("ExceptionStackTrace", logEvent.Exception.StackTrace);
            }

            foreach (var property in logEvent.Properties)
            {
                if (property.Value == null)
                    continue;

                if (properties.ContainsKey(property.Key) == false)
                    properties.Add(property.Key, property.Value.ToString());
                else
                {
                    // this isn't really elegant, but as as two property dictionaries are basically merged here, it's better to append rather than to overwrite/skip
                    properties.Add(property.Key + " #2", property.Value.ToString());
                }
            }

            // an finally - this logs the message & its metadata to application insights
            _telemetryClient.Track(traceTelemetry);
        }

        #endregion
    }
}
