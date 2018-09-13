using System.Web.Hosting;
namespace HelloWorldInfrastructure.Services
{
    public class ServerHostingEnvironmentService : IHostingEnvironmentService
    {
        public string MapPath(string path)
        {
            return HostingEnvironment.MapPath("~/" + path);
        }
    }
}