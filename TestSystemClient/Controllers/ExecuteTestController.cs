using AlertLibrary;
using DtoModels.Answers;
using DtoModels.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Mark;
using Services.QuestionService;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    public class ExecuteTestController : Controller
    {
        IQuestionService questionService;
        IMarkService markService;
        public ExecuteTestController(IQuestionService questionService, IMarkService markService)
        {
            this.questionService = questionService;
            this.markService = markService;
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Start(int id, string name)
        {
            var model = new TestWithQuestionsDto()
            {
                TestId = id,
                Test = name, 
            };
            var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var questions = await questionService.GetQuestionWithAnswers(id, token);

            if (questions == null)
            {
                this.AddAlertDanger("Error occured while getting questions");
                return RedirectToAction("Tests", "Test");
            }
            else
            {
                model.Questions = questions;
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Start([FromForm] IDictionary<string, IEnumerable<string>> answers, int testId)
        {
            try
            {
                var processedAnswers = new Dictionary<int, int>();
                int result = 0;
                foreach (var answer in answers)
                {
                    if (!int.TryParse(answer.Key, out result))
                        continue;
                    var selectedAnswer = answer.Value.Any() ? answer.Value.First() : "0";
                    processedAnswers.Add(int.Parse(answer.Key), int.Parse(selectedAnswer));
                }
                var answerModel = new TestAnswer()
                {
                    Answers = processedAnswers,
                    TestId = testId
                };
                var token = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
                var mark = await markService.GetMarkFromAnswers(answerModel, token);
                if (mark != -1)
                {
                    this.AddAlertInfo($"You got {mark}");
                }
                else
                {
                    this.AddAlertDanger("Error occurred while checking your answers");
                }
            }
            catch 
            {
                this.AddAlertDanger("Error occurred while checking your answers");
            }
            return RedirectToAction("Tests", "Test");
        }
    }
}
