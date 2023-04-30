using lb1.Dtos.Responses.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb1.Services.Abstractions
{
    public interface IRegisterService
    {
        Task<RegisterResponse> Register(string email, string password);
    }
}
