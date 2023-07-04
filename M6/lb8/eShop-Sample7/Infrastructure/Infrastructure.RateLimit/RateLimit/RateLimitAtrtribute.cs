using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RateLimit.RateLimit
{
    public class RateLimitAtrtribute: Attribute
    {
        public TimeSpan TimeLimit { get; set; }
        public int Quantity { get; set; }

        public RateLimitAtrtribute(TimeSpan timeSpan, int quantity) => (TimeLimit, Quantity) = (timeSpan, quantity);
    }
}
