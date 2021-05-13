using DtoModels.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestSystemClient.Controllers
{
    public class ExecuteTestController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Start(int id, string name)
        {
            var model = new TestWithQuestionsDto()
            {
                TestId = id,
                Test = name
            };
            return View(model);
        }
    }
}
