using DtoModels.Answers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Answer
{
    public interface IAnswerService
    {
        public Task<IList<AnswerDto>> GetAnswers(int questionId, string token);

        public Task<AnswerDto> UpdateAnswer(AnswerDto answer, string token);

        public Task<AnswerDto> CreateAnswer(AnswerDto answer, string token);

        public Task<bool> DeleteAnswer(AnswerDto answer, string token);
    }
}
