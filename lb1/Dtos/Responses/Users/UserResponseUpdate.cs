﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb1.Dtos.Responses.Users
{
    public class UserResponseUpdate: UserResponse
    {
        public DateTimeOffset CreatedAt { get; set; }
    }
}
