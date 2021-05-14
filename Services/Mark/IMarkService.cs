using DtoModels.Answers;
using DtoModels.Mark;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Mark
{
    public interface IMarkService
    {
        public Task<int> GetMarkFromAnswers(TestAnswer answer, string token);

        public Task<IEnumerable<MarkDto>> GetMarkForTest(int testId, string token);

        public Task<IEnumerable<MyMarkDto>> GetMarkForUser(string token);
    }
}
