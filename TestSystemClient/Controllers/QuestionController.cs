using AlertLibrary;
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
            if(question == null)
            {
                this.AddAlertDanger("Technical error occured while getting questions");
            }

            return View(question);
        }
    }
}
