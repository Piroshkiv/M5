using lb1.Dtos.Responses.Account.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb1.Dtos.Responses.Account
{
    public class LoginResponse : IAccountResponse
    {
        public string? Token { get; set; }
        public string? Error { get; set; }
    }
}
