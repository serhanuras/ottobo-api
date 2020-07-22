using Microsoft.AspNetCore.Mvc.Filters;
using Ottobo.Infrastructure.Extensions;

namespace Ottobo.Api.Attributes
{
    public class HasSensitiveDataAttribute:ActionFilterAttribute
    {
        private SensitiveDataInPlace _sensitiveDataInPlace;
        public HasSensitiveDataAttribute(SensitiveDataInPlace sensitiveDataInPlace = SensitiveDataInPlace.Both)
        {
            this._sensitiveDataInPlace = sensitiveDataInPlace;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Request.Headers["SensitiveDataInPlace"] = this._sensitiveDataInPlace.ToString();
        }
    }
}