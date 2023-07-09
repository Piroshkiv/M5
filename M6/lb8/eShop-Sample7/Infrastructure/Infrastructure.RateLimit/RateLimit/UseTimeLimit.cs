using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RateLimit.RateLimit
{
    public static class UseTimeLimit
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, int second, int quantity)
        {
            return builder.UseMiddleware<RateLimitMiddleware>(TimeSpan.FromSeconds(second), quantity);
        }
    }
}
