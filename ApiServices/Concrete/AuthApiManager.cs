using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InGame.WebUI.ApiServices.Interfaces;
using InGame.WebUI.Enum;
using InGame.WebUI.Models;
using InGame.WebUI.Models.Common;
using InGame.WebUI.Models.User;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InGame.WebUI.ApiServices.Concrete
{
    public class AuthApiManager : IAuthApiService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthApiManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResult> SignIn(UserLoginDto model)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                var jsonData = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(3000);
                    client.BaseAddress = new Uri("http://localhost:44360/api/Auth/");
                    var response = client.PostAsync("Login", content)
                        .ConfigureAwait(false);

                    try
                    {
                        var responseResult = response.GetAwaiter().GetResult().Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<ServiceResult>(responseResult);

                        if (result.ServiceResultType == ServiceResultType.Success)
                        {
                            var accessToken = result.Data as AccessToken;
                            _httpContextAccessor.HttpContext.Session.SetString("token", accessToken.Token);
                            serviceResult.ServiceResultType = ServiceResultType.Success;
                            return serviceResult;
                        }
                    }
                    catch (Exception exception)
                    {
                        serviceResult.ServiceResultType = ServiceResultType.Error;
                    }
                }

                serviceResult.ServiceResultType = ServiceResultType.Error;
                return serviceResult;
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = e.Message;
                return serviceResult;
            }


        }
    }
}
