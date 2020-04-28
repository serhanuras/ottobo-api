
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;


namespace Ottobo.Api.Filters
{

    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logging;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this._logging = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logging.LogError(context.Exception, context.Exception.Message);



            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            context.Result = new ObjectResult(new ErrorDto()
            {
                Message = "There is a problem on server side. Please try again later..."

            });

            context.ExceptionHandled = true;
            base.OnException(context);
        }


    }
}