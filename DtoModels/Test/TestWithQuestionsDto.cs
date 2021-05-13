using DtoModels.Question;
using System.Collections.Generic;

namespace DtoModels.Test
{
    public class TestWithQuestionsDto
    {
        public int TestId { get; set; }

        public string Test { get; set; }

        public IEnumerable<QuestionWithAnswersDto> Questions { get; set; }
    }
}
