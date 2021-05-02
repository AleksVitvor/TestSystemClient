using AlertLibrary;
using DtoModels.RequestModels.Test;
using DtoModels.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.TestService.Tests;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private ITestService TestService;

        public TestController(ITestService testService)
        {
            TestService = testService;
        }

        [HttpGet]
        [Authorize(Roles = "Student, Teacher, Admin")]
        public async Task<IActionResult> Tests()
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var tests = await TestService.GetTests(token);

            if (User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == "Student")
            {
                tests = tests.Where(x => x.IsActive == true);
            }

            if (tests==null)
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
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            if (await TestService.CreateTest(test, token))
            {
                this.AddAlertSuccess("Test added successfully");
            }
            else
                this.AddAlertDanger("Error occured while creating test");
            return RedirectToAction(nameof(Tests));
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int id)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var test = await TestService.GetOneTest(id, token);
            if (test == null)
            {
                this.AddAlertWarning("Can't find test");
                return RedirectToAction(nameof(Tests));
            }
            return View(test);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(TestRequestedModel test)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            if(await TestService.UpdateTest(test, token))
            {
                this.AddAlertSuccess("Test updated succesfully");
            }
            if (test == null)
            {
                this.AddAlertWarning("Can't update test");
            }
            return RedirectToAction(nameof(Tests));
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var test = new TestRequestedModel()
            {
                TestId = id,
                IsActive = false
            };

            if (await TestService.ChangeTestActivity(test, token))
            {
                this.AddAlertSuccess("Test deactivated successfully");
            }
            else
            {
                this.AddAlertWarning("Test wasn't deactivated");
            }
            return RedirectToAction(nameof(Tests));
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Activate(int id)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var test = new TestRequestedModel()
            {
                TestId = id,
                IsActive = true
            };

            if (await TestService.ChangeTestActivity(test, token))
            {
                this.AddAlertSuccess("Test activated successfully");
            }
            else
            {
                this.AddAlertWarning("Test wasn't activated");
            }
            return RedirectToAction(nameof(Tests));
        }
    }
}
