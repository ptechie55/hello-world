using System;
using System.IO;
using HelloWorldInfrastructure.Resources;
namespace HelloWorldInfrastructure.Services
{
    public class TextFileIOService : IFileIOService
    {
        private readonly IHostingEnvironmentService hostingEnvironmentService;
        public TextFileIOService(IHostingEnvironmentService hostingEnvironmentService)
        {
            this.hostingEnvironmentService = hostingEnvironmentService;
        }
        public string ReadFile(string filePath)
        {
            string fileContents;

            // Map path to server path
            var serverPath = this.hostingEnvironmentService.MapPath(filePath);

            // Read the contents of the file
            try
            {
                using (var reader = new StreamReader(serverPath))
                {
                    fileContents = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                // Throw an IO exception
                throw new IOException(ErrorCodes.HelloWorldFileError, new IOException("There was a problem reading the data file!", ex));
            }

            return fileContents;
        }
    }
}