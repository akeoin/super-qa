using Abp.Application.Services;
using AkeoIN.SuperQA.TestRuns.Dtos;

namespace AkeoIN.SuperQA.TestRuns
{
    public interface ITestRunAppService : IAsyncCrudAppService<TestRunDto, int, PagedTestRunResultRequestDto, CreateTestRunDto, TestRunDto>
    {
    }
} 