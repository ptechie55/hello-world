using System;

namespace HelloWorldInfrastructure.FrameworkWrappers
{

    public class SystemUri : IUri
    {
        public Uri GetUri(string uriString)
        {
            return new Uri(uriString);
        }
    }
}