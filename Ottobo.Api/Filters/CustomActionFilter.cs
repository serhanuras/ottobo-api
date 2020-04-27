using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Ottobo.Api.Filters
{

    public class CustomActionFilter:IActionFilter
    {
        private readonly ILogger _logging;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            this._logging = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logging.LogWarning("OnActionExecuting");

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logging.LogWarning("OnActionExecuted");
        }

        
    }
}