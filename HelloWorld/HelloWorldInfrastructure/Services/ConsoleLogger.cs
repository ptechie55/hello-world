using System;
using System.Collections.Generic;
using System.Text;
using HelloWorldInfrastructure.FrameworkWrappers;


namespace HelloWorldInfrastructure.Services
{
    public class ConsoleLogger : ILogger
    {
        private readonly IConsole console;
        public ConsoleLogger(IConsole console)
        {
            this.console = console;
        }
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog("INFO", message, otherProperties, null);
        }
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog("DEBUG", message, otherProperties, null);
        }
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.WriteLog("ERROR", message, otherProperties, exception);
        }
        private void WriteLog(string logLevel, string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            // Create a string builder with the log level and message
            var builder = new StringBuilder(logLevel);
            builder.Append(": ");
            builder.Append(message);

            // Check for other properties
            if (otherProperties != null)
            {
                foreach (var property in otherProperties)
                {
                    if (property.Key != null && property.Value != null)
                    {
                        builder.Append(" [");
                        builder.Append(property.Key);
                        builder.Append("=");
                        builder.Append(property.Value);
                        builder.Append("]");
                    }
                }
            }

            // Check for an exception
            if (exception != null)
            {
                builder.Append(" [Exception: ");
                builder.Append(exception);
                builder.Append("]");
            }

            // Write the log to the Console
            this.console.WriteLine(builder.ToString());
        }
    }
}