using lb1.Dtos.Responses.Account;
using lb1.Dtos.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb1.Services.Abstractions
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(string email, string password);
    }
}
