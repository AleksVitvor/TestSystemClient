using DtoModels.Question;
using System.Threading.Tasks;

namespace Services.QuestionService
{
    public interface IQuestionService
    {
        public Task<QuestionModelDto> GetQuestions(int testId, string token);
    }
}
