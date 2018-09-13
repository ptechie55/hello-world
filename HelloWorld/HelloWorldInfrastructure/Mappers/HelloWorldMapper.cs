using HelloWorldInfrastructure.Models;

namespace HelloWorldInfrastructure.Mappers
{
    public class HelloWorldMapper : IHelloWorldMapper
    {
        public HelloWorldInfraModel StringToData(string input)
        {
            return new HelloWorldInfraModel { Data = input };
        }
    }
}