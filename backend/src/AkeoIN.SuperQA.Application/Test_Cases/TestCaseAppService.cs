using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AkeoIN.SuperQA.Test_Cases.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AkeoIN.SuperQA.Test_Cases
{
    public class TestCaseAppService : AsyncCrudAppService<TestCase, TestCaseDto, int, PagedTestCaseResultRequestDto, CreateTestCaseDto, TestCaseDto>, ITestCaseAppService
    {
        public TestCaseAppService(IRepository<TestCase, int> repository) : base(repository)
        {
        }

        protected override IQueryable<TestCase> CreateFilteredQuery(PagedTestCaseResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Scenario)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Steps.Contains(input.Keyword) || x.ExpectedOutcome.Contains(input.Keyword))
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), x => x.Status == input.Status)
                .WhereIf(input.ScenarioId.HasValue, x => x.ScenarioId == input.ScenarioId.Value);
        }

        protected override async Task<TestCase> GetEntityByIdAsync(int id)
        {
            var testCase = await Repository.GetAllIncluding(x => x.Scenario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (testCase == null)
            {
                throw new EntityNotFoundException(typeof(TestCase), id);
            }

            return testCase;
        }

        protected override IQueryable<TestCase> ApplySorting(IQueryable<TestCase> query, PagedTestCaseResultRequestDto input)
        {
            return query.OrderBy(x => x.Name);
        }

        protected override TestCase MapToEntity(CreateTestCaseDto createInput)
        {
            return ObjectMapper.Map<TestCase>(createInput);
        }

        protected override void MapToEntity(TestCaseDto input, TestCase entity)
        {
            ObjectMapper.Map(input, entity);
        }

        protected override TestCaseDto MapToEntityDto(TestCase entity)
        {
            var testCaseDto = base.MapToEntityDto(entity);
            if (entity.Scenario != null)
            {
                testCaseDto.ScenarioName = entity.Scenario.Name;
            }
            return testCaseDto;
        }
    }
} 