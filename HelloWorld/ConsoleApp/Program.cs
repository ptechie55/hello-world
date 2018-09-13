using System;
using ConsoleApp.Services;
using HelloWorldInfrastructure.FrameworkWrappers;
using HelloWorldInfrastructure.Services;
using LightInject;
using RestSharp;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setup dependency injection and run the application
            using (var container = new ServiceContainer())
            {
                // Configure depenency injection
                container.Register<IHelloWorldConsoleApp, HelloWorldConsoleApp>();
                container.Register<IAppSettings, ConfigAppSettings>();
                container.Register<IConsole, SystemConsole>();
                container.Register<ILogger, ConsoleLogger>();
                container.Register<IUri, SystemUri>();
                container.Register<IHelloWorldWebService, HelloWorldWebService>();
                container.RegisterInstance(typeof(IRestClient), new RestClient());
                container.RegisterInstance(typeof(IRestRequest), new RestRequest());

                // Run the main program
                container.GetInstance<IHelloWorldConsoleApp>().Run(args);

          
            }
            Console.ReadLine();
        }
    }
}