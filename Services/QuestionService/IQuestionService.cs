using DtoModels.Question;
using System.Threading.Tasks;

namespace Services.QuestionService
{
    public interface IQuestionService
    {
        public Task<QuestionModelDto> GetQuestions(int testId, string token);

        public Task<bool> CreateQuestion(QuestionAddModel question, string token);

        public Task<bool> DeleteQuestion(int questionId, string token);
    }
}
