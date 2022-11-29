using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.WebUI.Models;
using InGame.WebUI.Models.Common;
using InGame.WebUI.Models.User;

namespace InGame.WebUI.ApiServices.Interfaces
{
    public interface IAuthApiService
    {
        Task<ServiceResult> SignIn(UserLoginDto model);
    }
}
