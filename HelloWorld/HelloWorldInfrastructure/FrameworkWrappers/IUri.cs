using System;
namespace HelloWorldInfrastructure.FrameworkWrappers
{
    public interface IUri
    {
        Uri GetUri(string uriString);
    }
}