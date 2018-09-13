using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Resources;
using HelloWorldInfrastructure.Services;

namespace HelloWorldInfrastructure.Attributes
{
    public enum SeverityCode
    {
        None,
        
        Information,
        
        Warning,
        
        Error,
        
        Critical
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public Type Type { get; set; }
        
        public HttpStatusCode Status { get; set; }
        
        public SeverityCode Severity { get; set; }
        
        public ILogger Logger { get; set; }
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            // If the exception type matches
            if (exception.GetType() == this.Type)
            {
                // Get the inner exception message if it exists
                var innerMessage = context.Exception.InnerException != null
                                       ? context.Exception.InnerException.Message
                                       : context.Exception.Message;

                // Create the error response without the stack trace/full exception (It can contain server path information)
                context.Response = context.Request.CreateResponse(
                    this.Status,
                    new ErrorResponse
                    {
                        Code = context.Exception.Message,
                        Message = innerMessage,
                        Type = context.Exception.GetType().ToString()
                    });

                // Log the error (including the stack trace/full exception)
                this.Logger.Error(innerMessage, null, context.Exception);
            }
            else
            {
                if ((this.Type == null) && (context.Response == null))
                {
                    // Unhandled exception (Critical InternalServerError)
                    context.Response = context.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new ErrorResponse
                        {
                            Code = ErrorCodes.GeneralError,
                            Message = context.Exception.Message,
                            Type = context.Exception.GetType().ToString()
                        });

                    // Log the error
                    this.Logger.Error(ErrorCodes.GeneralError, null, context.Exception);
                }
            }
        }
    }
}