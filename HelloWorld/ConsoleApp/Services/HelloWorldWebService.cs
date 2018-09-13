using System;
using HelloWorldInfrastructure.FrameworkWrappers;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Resources;
using HelloWorldInfrastructure.Services;
using RestSharp;

namespace ConsoleApp.Services
{
    public class HelloWorldWebService : IHelloWorldWebService
    {
        private readonly IAppSettings appSettings;
        
        private readonly ILogger logger;
        
        private readonly IRestClient restClient;
        
        private readonly IRestRequest restRequest;
        
        private readonly IUri uriService;
        
        public HelloWorldWebService(
            IRestClient restClient,
            IRestRequest restRequest,
            IAppSettings appSettings,
            IUri uriService,
            ILogger logger)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
            this.appSettings = appSettings;
            this.uriService = uriService;
            this.logger = logger;
        }
        
        public HelloWorldInfraModel GetData()
        {
            HelloWorldInfraModel modelData = null;

            // Set the URL for the request
            this.restClient.BaseUrl = this.uriService.GetUri(this.appSettings.Get(AppSettingsKeys.HelloWorldApiUrlKey));

            // Setup the request
            this.restRequest.Resource = "HelloWorld";
            this.restRequest.Method = Method.GET;

            // Clear the request parameters
            this.restRequest.Parameters.Clear();

            // Execute the call and get the response
            var response = this.restClient.Execute<HelloWorldInfraModel>(this.restRequest);

            // Check for data in the response
            if (response != null)
            {
                // Check if any actual data was returned
                if (response.Data != null)
                {
                    modelData = response.Data;
                }
                else
                {
                    var errorMessage = "Error in RestSharp, most likely in endpoint URL." + " Error message: "
                                       + response.ErrorMessage + " HTTP Status Code: "
                                       + response.StatusCode + " HTTP Status Description: "
                                       + response.StatusDescription;

                    // Check for existing exception
                    if (response.ErrorMessage != null && response.ErrorException != null)
                    {
                        // Log an informative exception including the RestSharp exception
                        this.logger.Error(errorMessage, null, response.ErrorException);
                    }
                    else
                    {
                        // Log an informative exception including the RestSharp content
                        this.logger.Error(errorMessage, null, new Exception(response.Content));
                    }
                }
            }
            else
            {
                // Log the exception
                const string ErrorMessage =
                    "Did not get any response from the Hello World Web Api for the Method: GET /helloworld";

                this.logger.Error(ErrorMessage, null, new Exception(ErrorMessage));
            }

            return modelData;
        }
    }
}