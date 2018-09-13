using HelloWorldInfrastructure.Models;

namespace ConsoleApp.Services
{
    public interface IHelloWorldWebService
    {
        HelloWorldInfraModel GetData();
    }
}