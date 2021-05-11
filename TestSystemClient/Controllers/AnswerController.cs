using AlertLibrary;
using DtoModels.Answers;
using DtoModels.RequestModels.Question;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Answer;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        IAnswerService answerService;

        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Answers(string question, int questionId, int testId, string test)
        {
            var model = new QuestionRequestModel()
            {
                Question = question,
                QuestionId = questionId,
                Test = test,
                TestId = testId
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ReadAnswers([DataSourceRequest] DataSourceRequest request, int questionId, int testId)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var answers = await answerService.GetAnswers(questionId, token);
            if (answers != null)
            {
                return Json(answers.ToDataSourceResult(request));
            }
            else
            {
                this.AddAlertDanger("Technical error occured");
            }

            return RedirectToAction("Questions", "Question", new { id = testId });
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateAnswer([DataSourceRequest] DataSourceRequest request, AnswerDto existAnswer, int testId)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var updatedAnswer = await answerService.UpdateAnswer(existAnswer, token);
            if (updatedAnswer == null)
            {
                this.AddAlertDanger("Technical error occured");
                return RedirectToAction("Questions", "Question", new { id = testId });
            }
            return Json(new[] { updatedAnswer }.ToDataSourceResult(request));
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteAnswer([DataSourceRequest] DataSourceRequest request, AnswerDto removedAnswer, int testId)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            if (!await answerService.DeleteAnswer(removedAnswer, token))
            {
                this.AddAlertDanger("Technical error occured");
                return RedirectToAction("Questions", "Question", new { id = testId });
            }
            return Json(new[] { removedAnswer }.ToDataSourceResult(request));
        }
        
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateAnswer([DataSourceRequest] DataSourceRequest request, AnswerDto newAnswer, int testId, [FromQuery] int questionId)
        {
            newAnswer.QuestionId = questionId;
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var createdAnswer = await answerService.CreateAnswer(newAnswer, token);
            if (createdAnswer == null)
            {
                this.AddAlertDanger("Technical error occured");
                return RedirectToAction("Questions", "Question", new { id = testId });
            }
            return Json(new[] { createdAnswer }.ToDataSourceResult(request));
        }
    }
}
