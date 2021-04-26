using AlertLibrary;
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
            if (await RegistrationService.Register(registerModel))
            {
                this.AddAlertSuccess("User created successfully");
                return RedirectToAction(nameof(Login));
            }              
            else
                this.AddAlertDanger("Error occured while creating user");
            return RedirectToAction(nameof(Register));
        }
    }
}