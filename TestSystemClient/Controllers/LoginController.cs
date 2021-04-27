using AlertLibrary;
using DtoModels.Login;
using DtoModels.RequestModels.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Login.Login;
using Services.Login.Registration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    public class LoginController : Controller
    {
        IRegistrationService RegistrationService;
        ILoginService LoginService;

        public LoginController(IRegistrationService registrationService, ILoginService loginService)
        {
            RegistrationService = registrationService;
            LoginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelDto loginModel)
        {
            var registeredUser = await LoginService.LogIn(loginModel);
            if (registeredUser == null)
            {
                this.AddAlertWarning("No such user exists");
                return RedirectToAction(nameof(Login));
            }
            else
            {

            }
            
            return RedirectToAction("Tests", "Test");                
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelDto registerModel)
        {
            if (await RegistrationService.Register(registerModel))
            {
                this.AddAlertSuccess("User created successfully");
                return RedirectToAction(nameof(Login));
            }              
            else
                this.AddAlertDanger("Error occured while creating user");
            return RedirectToAction(nameof(Register));
        }

        private async Task Authenticate(RequestLoginModel loginModel)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, loginModel.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, loginModel.Role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}