using ProjectOnion.Model.Models;
using ProjectOnion.MongoDB.Interface;
using ProjectOnion.Repository.Entity;
using ProjectOnion.Repository.Interface;

namespace ProjectOnion.Repository.Repository
{
    public class TestRepository : EntityRepository<Test, IEntityProvider<Test>>, ITestRepository
    {
        private IEntityProvider<Test> _testProvider;

        public TestRepository(IEntityProvider<Test> testProvider) : base(testProvider)
        {
            this._testProvider = testProvider;
        }
    }
}
