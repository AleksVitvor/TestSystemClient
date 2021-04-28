using AlertLibrary;
using DtoModels.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.TestService.Tests;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    public class TestController : Controller
    {
        private ITestService TestService;
        private string Token;

        public TestController(ITestService testService)
        {
            TestService = testService;
        }

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public async Task<IActionResult> Tests()
        {
            Token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var tests = await TestService.GetTests(Token);
            if(tests==null)
            {
                this.AddAlertDanger("Technical error occured while getting tests");
                return RedirectToAction("User, Login");
            }
            else
                return View(tests);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateTest()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateTest(TestModelDto test)
        {
            if (await TestService.CreateTest(test, Token))
            {
                this.AddAlertSuccess("Test added successfully");
            }
            else
                this.AddAlertDanger("Error occured while creating user");
            return RedirectToAction(nameof(Tests));
        }
    }
}
