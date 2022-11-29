using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.WebUI.ApiServices.Interfaces;
using InGame.WebUI.Enum;
using InGame.WebUI.Models;
using InGame.WebUI.Models.Common;
using InGame.WebUI.Models.User;

namespace InGame.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthApiService _authApiService;

        public AuthController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginDto model)
        {
            ServiceResult result = await _authApiService.SignIn(model);
            if (result.ServiceResultType==ServiceResultType.Success)
            {
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
