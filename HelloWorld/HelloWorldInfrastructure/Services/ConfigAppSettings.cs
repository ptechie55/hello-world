using System.Configuration;
namespace HelloWorldInfrastructure.Services
{
    public class ConfigAppSettings : IAppSettings
    {
        public string Get(string name)
        {
            return ConfigurationManager.AppSettings.Get(name);
        }
    }
}