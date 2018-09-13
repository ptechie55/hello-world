using System.Configuration;
using HelloWorldInfrastructure.FrameworkWrappers;
using HelloWorldInfrastructure.Mappers;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Resources;

namespace HelloWorldInfrastructure.Services
{
    public class HelloWorldDataService : IDataService
    {
        private readonly IAppSettings appSettings;
        private readonly IFileIOService fileIOService;
        private readonly IHelloWorldMapper helloWorldMapper;
        public HelloWorldDataService(
            IAppSettings appSettings,
            IFileIOService fileIOService,
            IHelloWorldMapper helloWorldMapper)
        {
            this.appSettings = appSettings;
            this.fileIOService = fileIOService;
            this.helloWorldMapper = helloWorldMapper;
        }
        public HelloWorldInfraModel GetData()
        {
            // Get the file path
            var filePath = this.appSettings.Get(AppSettingsKeys.HelloWorldFileKey);

            if (string.IsNullOrEmpty(filePath))
            {
                // No file path was found, throw exception
                throw new SettingsPropertyNotFoundException(
                    ErrorCodes.HelloWorldFileSettingsKeyError, 
                    new SettingsPropertyNotFoundException("The TodayDataFile settings key was not found or had no value."));
            }

            // Get the data from the file
            var rawData = this.fileIOService.ReadFile(filePath);

            // Map to the return type
            var data = this.helloWorldMapper.StringToData(rawData);

            return data;
        }
    }
}