using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectOnion.Http.WebApi.V1.ViewModel.Entity;
using ProjectOnion.Model.Models;

namespace ProjectOnion.Http.WebApi.V1.ViewModel.TestViewData
{
    public class TestGetViewModel : EntityConverter<Test>
    {
        public TestGetViewModel()
        {
        }

        public TestGetViewModel(Test test):base(test)
        {
        }

        public Guid Id { get; set; }

        public string TestName { get; set; }
    }
}
