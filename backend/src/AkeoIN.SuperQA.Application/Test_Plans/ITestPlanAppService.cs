using Abp.Application.Services;
using AkeoIN.SuperQA.Test_Plans.Dtos;

namespace AkeoIN.SuperQA.Test_Plans
{
    public interface ITestPlanAppService : IAsyncCrudAppService<TestPlanDto, int, PagedTestPlanResultRequestDto, CreateTestPlanDto, TestPlanDto>
    {
    }
} 