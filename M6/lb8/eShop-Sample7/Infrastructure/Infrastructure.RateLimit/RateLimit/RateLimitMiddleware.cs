using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RateLimit.RateLimit
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        public TimeSpan TimeLimit { get; set; }
        public int? Quantity { get; set; }

        public RateLimitMiddleware(RequestDelegate next, TimeSpan timeSpan, int quantity)
        {
            this._next = next;
            this.TimeLimit = timeSpan;
            this.Quantity = quantity;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var redisConnection = context.RequestServices.GetService<IRedisCacheConnectionService>();
            var redisDb = redisConnection.Connection.GetDatabase();

            var httpContext = context!;

            var routeEndpoint = httpContext.GetEndpoint();
            var ip = httpContext.Connection.RemoteIpAddress.ToString();

            var key = $"{ip}";
            var currentRequestCount = redisDb.StringIncrement(key);

            if (currentRequestCount == 1)
            {
                redisDb.KeyExpire(key, TimeLimit);
            }
            if (currentRequestCount > (Quantity ?? 10))
            {
                context.Response.StatusCode = 429;
            }
            await _next.Invoke(context);
        }
    }
}
