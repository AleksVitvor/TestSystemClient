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
    }
}
