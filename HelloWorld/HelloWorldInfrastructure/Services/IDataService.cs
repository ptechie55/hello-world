using HelloWorldInfrastructure.Models;

namespace HelloWorldInfrastructure.Services
{
    public interface IDataService
    {
        HelloWorldInfraModel GetData();
    }
}