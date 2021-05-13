using DtoModels.Answers;
using System.Collections.Generic;

namespace DtoModels.Question
{
    public class QuestionWithAnswersDto
    {
        public string Question { get; set; }

        public int QuestionId { get; set; }

        public IEnumerable<AnswersForStudentsDto> Answers { get; set; }
    }
}
