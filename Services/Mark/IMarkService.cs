using DtoModels.Answers;
using System.Threading.Tasks;

namespace Services.Mark
{
    public interface IMarkService
    {
        public Task<int> GetMarkFromAnswers(TestAnswer answer, string token);
    }
}
