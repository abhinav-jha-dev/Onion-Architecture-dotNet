using ProjectOnion.Model.Models;
using ProjectOnion.Repository.Interface;
using ProjectOnion.Service.Entity;
using ProjectOnion.Service.Interface;

namespace ProjectOnion.Service.Service
{
    public class TestService : EntityService<Test>, ITestService
    {
        private ITestRepository _testRepository;
        public TestService(ITestRepository testRepository) : base(testRepository)
        {
            this._testRepository = testRepository;
        }
    }
}
