using DtoModels.RequestModels.Test;
using DtoModels.Test;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.TestService.Tests
{
    public interface ITestService
    {
        public Task<IEnumerable<TestRequestedModel>> GetTests(string token);

        public Task<bool> CreateTest(TestModelDto test, string token);

        public Task<TestRequestedModel> GetOneTest(int id, string token);

        public Task<bool> UpdateTest(TestRequestedModel test, string token);

        public Task<bool> ChangeTestActivity(TestRequestedModel test, string token);
    }
}
