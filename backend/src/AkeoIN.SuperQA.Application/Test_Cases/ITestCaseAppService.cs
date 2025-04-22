using System;
using Abp.Application.Services;
using AkeoIN.SuperQA.Test_Cases.Dtos;

namespace AkeoIN.SuperQA.Test_Cases
{
    public interface ITestCaseAppService : IAsyncCrudAppService<TestCaseDto, int, PagedTestCaseResultRequestDto, CreateTestCaseDto, TestCaseDto>
    {
    }
} 