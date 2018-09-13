using HelloWorldInfrastructure.Mappers;
using HelloWorldInfrastructure.Models;
using NUnit.Framework;

namespace HelloWorldAPI.Tests.UnitTests
{
    [TestFixture]
    public class HelloWorldMapperUnitTests
    {
        private HelloWorldMapper helloWorldMapper;

        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Create object to test
            this.helloWorldMapper = new HelloWorldMapper();
        }

        #region StringToData Tests

        [Test]
        public void UnitTestHelloWorldMapperStringToDataNormalSuccess()
        {
            const string Data = "Hello World!";

            // Create the expected result
            var expectedResult = GetSampleData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }
        [Test]
        public void UnitTestHelloWorldMapperStringToDataNullSuccess()
        {
            const string Data = null;

            // Create the expected result
            var expectedResult = GetSampleData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }
        #endregion

        #region Helper Methods
        private static HelloWorldInfraModel GetSampleData(string data)
        {
            return new HelloWorldInfraModel()
            {
                Data = data
            };
        }
        #endregion
    }
}