using System.Collections.Generic;

namespace DtoModels.Question
{
    public class QuestionModelDto
    {
        public int TestId { get; set; }

        public string Test { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
