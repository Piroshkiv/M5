using lb1.Config;
using lb1.Dtos.Requests;
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

namespace lb1.Services
{
    internal class RegisterService : IRegisterService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<RegisterService> _logger;
        private readonly ApiOption _options;
        private readonly string _registerApi = "api/register";

        public RegisterService(
            IInternalHttpClientService httpClientService,
            IOptions<ApiOption> options,
            ILogger<RegisterService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<RegisterResponse> Register( string email, string password)
        {
            var result = await _httpClientService.GetAsync<AccountRequest>(
           $"{_options.Host}{_registerApi}/",
           HttpMethod.Post,
           new AccountRequest()
           {
               Email = email,
               Password = password
           });

            if (((int)result.StatusCode) == 200)
            {
                var response = await _httpClientService.DeserializeResponse<RegisterResponse>(result);
                _logger.LogInformation($"User {email} was register");
                return response;
            }
            else if (((int)result.StatusCode) == 400)
            {
                var response = await _httpClientService.DeserializeResponse<ErrorResponse>(result);
                _logger.LogInformation(response.Error);
            }

            return default(RegisterResponse)!;
        }
    }
}
