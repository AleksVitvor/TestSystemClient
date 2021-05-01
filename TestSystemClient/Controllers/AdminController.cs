using Microsoft.AspNetCore.Mvc;
using Services.User;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using DtoModels.User;
using AlertLibrary;

namespace TestSystemClient.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IUserService UserService;

        public AdminController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var users = await UserService.GetUsers(token);
            if (users != null)
            {
                return View(users);
            }
            else
                return RedirectToAction("Tests", "Test");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(UserModelDto user)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            if(await UserService.ChangeUserRole(user, token))
            {
                this.AddAlertSuccess("User role changed");
            }
            else
            {
                this.AddAlertDanger("User role wasn't changed");
            }

            return RedirectToAction(nameof(Users));
        }
    }
}
