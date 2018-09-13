using System;
using System.Configuration;
using System.IO;
using HelloWorldInfrastructure.FrameworkWrappers;
using HelloWorldInfrastructure.Mappers;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Resources;
using HelloWorldInfrastructure.Services;
using Moq;
using NUnit.Framework;

namespace HelloWorldAPI.Tests.UnitTests
{
    [TestFixture]
    public class HelloWorldDataServiceUnitTests
    {
        private Mock<IAppSettings> appSettingsMock;
        
        private Mock<IFileIOService> fileIOServiceMock;
        
        private Mock<IHelloWorldMapper> helloWorldMapperMock;
        
        private HelloWorldDataService helloWorldDataService;
        
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.appSettingsMock = new Mock<IAppSettings>();
            this.fileIOServiceMock = new Mock<IFileIOService>();
            this.helloWorldMapperMock = new Mock<IHelloWorldMapper>();

            // Create object to test
            this.helloWorldDataService = new HelloWorldDataService(
                this.appSettingsMock.Object,
                this.fileIOServiceMock.Object,
                this.helloWorldMapperMock.Object);
        }

        #region Tests
        [Test]
        public void UnitTestHelloWorldServiceSuccess()
        {
            // Create return models for dependencies
            const string filePath = "fakePath";
            const string data = "Hello World!";
            
            // Create the expected result
            var expectedResult = GetSampleData(data);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldFileKey)).Returns(filePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(filePath)).Returns(data);
            this.helloWorldMapperMock.Setup(m => m.StringToData(data)).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldDataService.GetData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.HelloWorldFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetDataSettingKeyNull()
        {
            // Create return models for dependencies
            const string filePath = null;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldFileKey)).Returns(filePath);

            // Call the method to test
            this.helloWorldDataService.GetData();
        }
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.HelloWorldFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetDataSettingKeyEmptyString()
        {
            // Create return models for dependencies
            var filePath = string.Empty;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldFileKey)).Returns(filePath);

            // Call the method to test
            this.helloWorldDataService.GetData();
        }
        [Test]
        [ExpectedException(ExpectedException = typeof(IOException))]
        public void UnitTestHelloWorldDataServiceGetDataIOException()
        {
            // Create return models for dependencies
            const string filePath = "fakePath";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldFileKey)).Returns(filePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(filePath)).Throws(new IOException("Error!"));

            // Call the method to test
            this.helloWorldDataService.GetData();
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