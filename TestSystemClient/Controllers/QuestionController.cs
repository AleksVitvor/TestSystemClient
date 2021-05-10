using AlertLibrary;
using DtoModels.Question;
using DtoModels.RequestModels.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.QuestionService;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        IQuestionService QuestionService;
        public QuestionController(IQuestionService questionService)
        {
            QuestionService = questionService;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Questions(int id)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var question = await QuestionService.GetQuestions(id, token);
            if (question == null)
            {
                this.AddAlertDanger("Technical error occured while getting questions");
            }

            return View(question);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateQuestion(int testId, string test)
        {
            var model = new QuestionRequestModel()
            {
                TestId = testId,
                Test = test
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateQuestion(QuestionAddModel newQuestion)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            if(!await QuestionService.CreateQuestion(newQuestion, token))
            {
                this.AddAlertDanger("Technical error occured while creating question");
            }
            return RedirectToAction(nameof(Questions), new { id = newQuestion.TestId });
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int questionId, int testId)
        {
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            if(!await QuestionService.DeleteQuestion(questionId, token))
            {
                this.AddAlertDanger("Technical error occured while delete question");
            }
            return RedirectToAction(nameof(Questions), new { id = testId });
        }
    }
}
