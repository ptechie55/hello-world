using System;
using System.Collections.Generic;
using log4net.Config;
using log4net.Core;
namespace HelloWorldInfrastructure.Services
{
    public class JsonL4NLogger : ILogger
    {
        private readonly log4net.Core.ILogger log4NetLogger;
        private string loggerName;
        public JsonL4NLogger()
        {
            XmlConfigurator.Configure();
            this.log4NetLogger = LoggerManager.GetLogger(this.GetType().Assembly, this.GetType().Name);
            this.loggerName = this.GetType().Name;
        }
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog(Level.Info, message, otherProperties, null);
        }
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog(Level.Debug, message, otherProperties, null);
        }
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.WriteLog(Level.Error, message, otherProperties, exception);
        }
        private void WriteLog(Level logLevel, string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            // Create the logging event data
            var loggingEventData = new LoggingEventData()
            {
                Level = logLevel,
                LoggerName = this.loggerName,
                Domain = AppDomain.CurrentDomain.FriendlyName,
                TimeStamp = DateTime.Now,
                Message = message
            };

            // Create the logging event
            var loggingEvent = new LoggingEvent(loggingEventData);

            // Check for other properties
            if (otherProperties != null)
            {
                foreach (var property in otherProperties)
                {
                    if (property.Key != null && property.Value != null)
                    {
                        loggingEvent.Properties[property.Key] = property.Value;
                    }
                }
            }

            // Check for exception
            if (exception != null)
            {
                loggingEvent.Properties["exception"] = exception.ToString();
            }

            // Log the data
            this.log4NetLogger.Log(loggingEvent);
        }
    }
}
