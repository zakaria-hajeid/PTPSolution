using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Core.Common.Attributes
{
    public class SecurityValidToken : Attribute, IActionFilter
    {
        private readonly SecurityApi configuration;
        public SecurityValidToken(IOptions<SecurityApi> options)
        {
            this.configuration = options.Value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string authorization = context.HttpContext.Request.Headers["SecurityApiKey"];
            if (string.IsNullOrEmpty(authorization))
            {
                throw new UnauthorizedAccessException();
            }

            string ApiKey = configuration.ApiKey;
            if (authorization != ApiKey)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
