using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace Infrastructure.RateLimit.RateLimit
{
    public class RateLimitFilter: ActionFilterAttribute
    {
        public RateLimitFilter(int second, int quantity) => (_timeLimit, _quantity) = (TimeSpan.FromSeconds(second), quantity);

        private TimeSpan _timeLimit { get; set; }
        private int? _quantity { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var redisConnection = context.HttpContext.RequestServices.GetService<IRedisCacheConnectionService>();
            var redisDb = redisConnection.Connection.GetDatabase();

            var httpContext = context.HttpContext!;

            var routeEndpoint = httpContext.GetEndpoint();
            var ip = httpContext.Connection.RemoteIpAddress.ToString();

            var key = $"{ip}";
            var currentRequestCount = redisDb.StringIncrement(key);

            if(currentRequestCount == 1)
            {
                redisDb.KeyExpire(key, _timeLimit);
            }
            if (currentRequestCount > (_quantity ?? 10))
            {
                context.Result = new StatusCodeResult(429);
            }
            base.OnActionExecuting(context!);
        }
    }
}
