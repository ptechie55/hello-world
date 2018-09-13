using System.Configuration;
using System.IO;
using HelloWorldAPI.Controllers;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Services;
using Moq;
using NUnit.Framework;

namespace HelloWorldAPI.Tests.UnitTests
{
    [TestFixture]
    public class HelloWorldControllerUnitTests
    {
        private Mock<IDataService> dataServiceMock;
        
        private HelloWorldController DataController;
        
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.dataServiceMock = new Mock<IDataService>();

            // Create object to test
            this.DataController = new HelloWorldController(this.dataServiceMock.Object);
        }

        #region Get Tests

        [Test]
        public void UnitTestControllerGetSuccess()
        {
            // Create the expected result
            var expectedResult = GetSampleData();

            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetData()).Returns(expectedResult);

            // Call the method to test
            var result = this.DataController.Get();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException))]
        public void UnitTestDataControllerGetSettingsPropertyNotFoundException()
        {
            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetData()).Throws(new SettingsPropertyNotFoundException("Error!"));

            // Call the method to test
            this.DataController.Get();
        }
        [Test]
        [ExpectedException(ExpectedException = typeof(IOException))]
        public void UnitTestDataControllerGetIOException()
        {
            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetData()).Throws(new IOException("Error!"));

            // Call the method to test
            this.DataController.Get();
        }
        #endregion

        #region Helper Methods
        private static HelloWorldInfraModel GetSampleData()
        {
            return new HelloWorldInfraModel()
            {
                Data = "Hello World!"
            };
        }
        #endregion
    }
}