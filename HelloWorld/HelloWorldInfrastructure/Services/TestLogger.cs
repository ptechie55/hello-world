using System;
using System.Collections.Generic;
namespace HelloWorldInfrastructure.Services
{
    public class TestLogger : ILogger
    {
        private readonly List<string> logMessageList;
        private readonly List<Exception> exceptionList;
        private readonly List<object> otherPropertiesList;
        public TestLogger(ref List<string> logMessageList, ref List<Exception> exceptionList, ref List<object> otherPropertiesList)
        {
            this.logMessageList = logMessageList;
            this.exceptionList = exceptionList;
            this.otherPropertiesList = otherPropertiesList;
        }
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
        }
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
        }
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
            this.exceptionList.Add(exception);
        }
    }
}