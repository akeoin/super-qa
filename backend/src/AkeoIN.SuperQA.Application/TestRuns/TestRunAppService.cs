using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AkeoIN.SuperQA.TestRuns.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AkeoIN.SuperQA.TestRuns
{
    public class TestRunAppService : AsyncCrudAppService<TestRun, TestRunDto, int, PagedTestRunResultRequestDto, CreateTestRunDto, TestRunDto>, ITestRunAppService
    {
        public TestRunAppService(IRepository<TestRun, int> repository) : base(repository)
        {
        }

        protected override IQueryable<TestRun> CreateFilteredQuery(PagedTestRunResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.TestCase, x => x.TestResults)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword))
                .WhereIf(input.TestCaseId.HasValue, x => x.TestCaseId == input.TestCaseId.Value);
        }

        protected override async Task<TestRun> GetEntityByIdAsync(int id)
        {
            var testRun = await Repository.GetAllIncluding(x => x.TestCase, x => x.TestResults)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (testRun == null)
            {
                throw new EntityNotFoundException(typeof(TestRun), id);
            }

            return testRun;
        }

        protected override IQueryable<TestRun> ApplySorting(IQueryable<TestRun> query, PagedTestRunResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }

        protected override TestRun MapToEntity(CreateTestRunDto createInput)
        {
            return ObjectMapper.Map<TestRun>(createInput);
        }

        protected override void MapToEntity(TestRunDto input, TestRun entity)
        {
            ObjectMapper.Map(input, entity);
        }

        protected override TestRunDto MapToEntityDto(TestRun entity)
        {
            var testRunDto = base.MapToEntityDto(entity);
            testRunDto.TestResultCount = entity.TestResults?.Count ?? 0;
            return testRunDto;
        }
    }
} 