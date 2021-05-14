using AlertLibrary;
using DtoModels.Test;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Mark;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    public class MarkController : Controller
    {
        IMarkService markService;

        public MarkController(IMarkService markService)
        {
            this.markService = markService;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetMarks(TestModelWithIdDto testModel)
        {
            return View(testModel);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ReadMarks([DataSourceRequest] DataSourceRequest request, int testId)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var marks = await markService.GetMarkForTest(testId, token);
            if (marks == null)
            {
                this.AddAlertDanger("Technical error occured");
                return RedirectToAction("Tests", "Test");
            }

            return Json(marks.ToDataSourceResult(request));
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyMarks()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> ReadMyMarks([DataSourceRequest] DataSourceRequest request)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var marks = await markService.GetMarkForUser(token);
            if (marks == null)
            {
                this.AddAlertDanger("Technical error occured");
                return RedirectToAction("Tests", "Test");
            }

            return Json(marks.ToDataSourceResult(request));
        }
    }
}
