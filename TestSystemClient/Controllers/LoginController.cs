using DtoModels.Login;
using Microsoft.AspNetCore.Mvc;
using Services.Login.Registration;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    public class LoginController : Controller
    {
        IRegistrationService RegistrationService;

        public LoginController(IRegistrationService registrationService)
        {
            RegistrationService = registrationService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelDto registerModel)
        {
            await RegistrationService.Register(registerModel);
            return new RedirectResult("/Login/Login");
        }
    }
}