using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Http;
using HelloWorldInfrastructure.Attributes;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Services;


namespace HelloWorldAPI.Controllers
{
    [WebApiExceptionFilter]
    public class HelloWorldController : ApiController
    {
        private readonly IDataService dataService;
        public HelloWorldController(IDataService dataService)
        {
            this.dataService = dataService;
        }
        
        [WebApiExceptionFilter(Type = typeof(IOException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        [WebApiExceptionFilter(Type = typeof(SettingsPropertyNotFoundException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        public HelloWorldInfraModel Get()
        {
            return this.dataService.GetData();
        }
    }
}