using lb1.Config;
using lb1.Dtos.Responses;
using lb1.Dtos;
using lb1.Dtos.Responses.Account;
using lb1.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lb1.Dtos.Requests;
using lb1.Dtos.Responses.Users;
using System.Xml.Linq;

namespace lb1.Services
{
    public class LoginService : ILoginService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<LoginService> _logger;
        private readonly ApiOption _options;
        private readonly string _loginApi = "api/login";

        public LoginService(
            IInternalHttpClientService httpClientService,
            IOptions<ApiOption> options,
            ILogger<LoginService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            var result = await _httpClientService.GetAsync<AccountRequest>(
           $"{_options.Host}{_loginApi}/",
           HttpMethod.Post,
           new AccountRequest()
           {
               Email = email,
               Password = password
           });

            if (((int)result.StatusCode) == 200)
            {
                var response = await _httpClientService.DeserializeResponse<LoginResponse>(result);
                _logger.LogInformation($"User {email} was login");
                return response;
            }
            else if(((int)result.StatusCode) == 400)
            {
                var response = await _httpClientService.DeserializeResponse<ErrorResponse>(result);
                _logger.LogInformation(response.Error);
            }

            return default(LoginResponse) !;
        }

    }
}
