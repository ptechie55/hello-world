using ConsoleApp.Services;
using HelloWorldInfrastructure.Services;

namespace ConsoleApp
{
    public class HelloWorldConsoleApp : IHelloWorldConsoleApp
    {

        private readonly IHelloWorldWebService helloWorldWebService;
        private readonly ILogger logger;
        public HelloWorldConsoleApp(IHelloWorldWebService helloWorldWebService, ILogger logger)
        {
            this.helloWorldWebService = helloWorldWebService;
            this.logger = logger;
        }
        public void Run(string[] arguments)
        {
            // Get data
            var data = this.helloWorldWebService.GetData();

            // Write Today's data to the screen
            this.logger.Info(data != null ? data.Data : "No data was found!", null);
        }
    }
}