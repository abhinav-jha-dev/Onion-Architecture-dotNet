using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectOnion.Http.WebApi.V1.ViewModel.Entity;
using ProjectOnion.Model.Models;

namespace ProjectOnion.Http.WebApi.V1.ViewModel.TestViewData
{
    public class TestCreateViewModel: EntityConverter<Test>
    {
        public TestCreateViewModel()
        {
        }

        public TestCreateViewModel(Test test):base(test)
        {
        }

        public string TestName { get; set; }
    }
}
