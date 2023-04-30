using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb1.Dtos.Responses.Users
{
    public class UserResponseCreate:UserResponse
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
