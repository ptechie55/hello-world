using HelloWorldInfrastructure.Models;
namespace HelloWorldInfrastructure.Mappers
{
    public interface IHelloWorldMapper
    {
        HelloWorldInfraModel StringToData(string input);
    }
}