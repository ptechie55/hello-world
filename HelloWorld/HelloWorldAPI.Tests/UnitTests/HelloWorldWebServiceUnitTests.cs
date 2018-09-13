using System;
using System.Collections.Generic;
using System.Net;
using ConsoleApp.Services;
using HelloWorldInfrastructure.FrameworkWrappers;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Resources;
using HelloWorldInfrastructure.Services;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace HelloWorldAPI.Tests.UnitTests
{
    [TestFixture]
    public class HelloWorldWebServiceUnitTests
    {
        private List<string> logMessageList;
        
        private List<Exception> exceptionList;
        
        private List<object> otherPropertiesList;
        
        private Mock<IAppSettings> appSettingsMock;
        
        private ILogger testLogger;
        
        private Mock<IRestClient> restClientMock;
        
        private Mock<IRestRequest> restRequestMock;
        
        private Mock<IUri> uriServiceMock;
        
        private HelloWorldWebService helleHelloWorldWebService;
        
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.appSettingsMock = new Mock<IAppSettings>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);
            this.restClientMock = new Mock<IRestClient>();
            this.restRequestMock = new Mock<IRestRequest>();
            this.uriServiceMock = new Mock<IUri>();

            // Create object to test
            this.helleHelloWorldWebService = new HelloWorldWebService(
                this.restClientMock.Object,
                this.restRequestMock.Object,
                this.appSettingsMock.Object,
                this.uriServiceMock.Object,
                this.testLogger);
        }
        [TearDown]
        public void TearDown()
        {
            // Clear lists
            this.logMessageList.Clear();
            this.exceptionList.Clear();
            this.otherPropertiesList.Clear();
        }

        #region GetData Tests

        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            // Create return models for dependencies
            const string Data = "Hello World!";
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<HelloWorldInfraModel>>();
            var sampleData = GetSampleData(Data);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<HelloWorldInfraModel>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(sampleData);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetData();

            // Check values
            Assert.NotNull(response);
            Assert.AreEqual(response.Data, sampleData.Data);
        }
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullResponse()
        {
            // Create return models for dependencies
            const string Data = "Hello World!";
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = (IRestResponse<HelloWorldInfraModel>)null;
            var sampleData = GetSampleData(Data);
            const string ErrorMessage = "Did not get any response from the Hello World Web Api for the Method: GET /helloworld";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<HelloWorldInfraModel>(It.IsAny<IRestRequest>())).Returns(mockRestResponse);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], ErrorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ErrorMessage);
        }
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullData()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<HelloWorldInfraModel>>();
            HelloWorldInfraModel modelData = null;
            const string ErrorMessage = "Error Message";
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            var errorException = new Exception("errorHere");
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL." 
                + " Error message: " + ErrorMessage 
                + " HTTP Status Code: " + StatusCode 
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<HelloWorldInfraModel>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(modelData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, errorException.Message);
        }
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullDataNullErrorMessage()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<HelloWorldInfraModel>>();
            HelloWorldInfraModel modelData = null;
            const string ErrorMessage = null;
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            var errorException = new Exception("errorHere");
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL."
                + " Error message: " + ErrorMessage
                + " HTTP Status Code: " + StatusCode
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<HelloWorldInfraModel>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(modelData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ProfileContent);
        }
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullDataNullErrorException()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<HelloWorldInfraModel>>();
            HelloWorldInfraModel modelData = null;
            const string ErrorMessage = "Error Message";
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            Exception errorException = null;
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL."
                + " Error message: " + ErrorMessage
                + " HTTP Status Code: " + StatusCode
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<HelloWorldInfraModel>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(modelData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ProfileContent);
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