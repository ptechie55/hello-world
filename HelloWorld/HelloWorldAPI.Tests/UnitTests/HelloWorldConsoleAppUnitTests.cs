using System;
using System.Collections.Generic;
using ConsoleApp;
using ConsoleApp.Services;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Services;
using Moq;
using NUnit.Framework;

namespace HelloWorldAPI.Tests.UnitTests
{
    [TestFixture]
    public class HelloWorldConsoleAppUnitTests
    {
        private List<string> logMessageList;
        
        private List<Exception> exceptionList;
        
        private List<object> otherPropertiesList;
        
        private Mock<IHelloWorldWebService> helloWorldWebServiceMock;
        
        private ILogger testLogger;
        
        private HelloWorldConsoleApp helloWorldConsoleApp;
        
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.helloWorldWebServiceMock = new Mock<IHelloWorldWebService>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);

            // Create object to test
            this.helloWorldConsoleApp = new HelloWorldConsoleApp(this.helloWorldWebServiceMock.Object, this.testLogger);
        }

        [TearDown]
        public void TearDown()
        {
            // Clear lists
            this.logMessageList.Clear();
            this.exceptionList.Clear();
            this.otherPropertiesList.Clear();
        }

        #region Run Tests
        
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            const string Data = "Hello World!";

            // Create return models for dependencies
            var sampleData = GetSampleData(Data);

            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetData()).Returns(sampleData);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], Data);
        }
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNullDataSuccess()
        {
            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetData()).Returns((HelloWorldInfraModel)null);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], "No data was found!");
        }
        #endregion
        
        #region Helper Methods
        private static HelloWorldInfraModel GetSampleData(string data)
        {
            return new HelloWorldInfraModel { Data = data };
        }
        #endregion
    }
}