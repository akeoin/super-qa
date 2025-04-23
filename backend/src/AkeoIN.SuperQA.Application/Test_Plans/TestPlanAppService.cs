using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AkeoIN.SuperQA.Test_Plans.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AkeoIN.SuperQA.Test_Plans
{
    public class TestPlanAppService : AsyncCrudAppService<TestPlan, TestPlanDto, int, PagedTestPlanResultRequestDto, CreateTestPlanDto, TestPlanDto>, ITestPlanAppService
    {
        public TestPlanAppService(IRepository<TestPlan, int> repository) : base(repository)
        {
        }

        protected override IQueryable<TestPlan> CreateFilteredQuery(PagedTestPlanResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.TestPlanTestCases)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword))
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), x => x.Status == input.Status);
        }

        protected override async Task<TestPlan> GetEntityByIdAsync(int id)
        {
            var testPlan = await Repository.GetAllIncluding(x => x.TestPlanTestCases)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (testPlan == null)
            {
                throw new EntityNotFoundException(typeof(TestPlan), id);
            }

            return testPlan;
        }

        protected override IQueryable<TestPlan> ApplySorting(IQueryable<TestPlan> query, PagedTestPlanResultRequestDto input)
        {
            return query.OrderBy(x => x.Name);
        }

        protected override TestPlan MapToEntity(CreateTestPlanDto createInput)
        {
            return ObjectMapper.Map<TestPlan>(createInput);
        }

        protected override void MapToEntity(TestPlanDto input, TestPlan entity)
        {
            ObjectMapper.Map(input, entity);
        }

        protected override TestPlanDto MapToEntityDto(TestPlan entity)
        {
            var testPlanDto = base.MapToEntityDto(entity);
            testPlanDto.TestCaseCount = entity.TestPlanTestCases?.Count ?? 0;
            return testPlanDto;
        }
    }
} 