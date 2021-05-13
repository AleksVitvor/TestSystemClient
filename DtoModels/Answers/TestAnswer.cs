using System.Collections.Generic;

namespace DtoModels.Answers
{
    public class TestAnswer
    {
        public int TestId { get; set; }

        public IDictionary<int, int> Answers { get; set; }
    }
}
