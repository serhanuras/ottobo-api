using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Ottobo.Api.RouteResult
{
     /// <summary>
    /// An <see cref="ActionResult"/> that returns a Created (201) response with a Location header.
    /// </summary>
    [DefaultStatusCode(DefaultStatusCode)]
    public class CustomCreatedAtRouteResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status201Created;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedAtRouteResult"/> class with the values
        /// provided.
        /// </summary>
        /// <param name="routeName">The name of the route to use for generating the URL.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <param name="value">The value to format in the entity body.</param>
        public CustomCreatedAtRouteResult(
            long id,
            [ActionResultObjectValue] object value)
            : base(value)
        {
            Id = id;
        }
        

       
        /// <summary>
        /// Gets or sets the route data to use for generating the URL.
        /// </summary>
        public long Id { get; set; }

        /// <inheritdoc />
        public override void OnFormatting(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            base.OnFormatting(context);


            var url = context.HttpContext.Request.GetDisplayUrl() + "/" + Id.ToString();

            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidOperationException();
            }

            context.HttpContext.Response.Headers[HeaderNames.Location] = url;
        }
    }
}