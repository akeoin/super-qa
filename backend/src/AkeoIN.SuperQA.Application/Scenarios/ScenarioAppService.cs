using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AkeoIN.SuperQA.Scenarios.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AkeoIN.SuperQA.Scenarios
{
    public class ScenarioAppService : AsyncCrudAppService<Scenario, ScenarioDto, int, PagedScenarioResultRequestDto, CreateScenarioDto, ScenarioDto>, IScenarioAppService
    {
        public ScenarioAppService(IRepository<Scenario, int> repository) : base(repository)
        {
        }

        protected override IQueryable<Scenario> CreateFilteredQuery(PagedScenarioResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Feature)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword))
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), x => x.Status == input.Status)
                .WhereIf(input.FeatureId.HasValue, x => x.FeatureId == input.FeatureId.Value);
        }

        protected override async Task<Scenario> GetEntityByIdAsync(int id)
        {
            var scenario = await Repository.GetAllIncluding(x => x.Feature)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (scenario == null)
            {
                throw new EntityNotFoundException(typeof(Scenario), id);
            }

            return scenario;
        }

        protected override IQueryable<Scenario> ApplySorting(IQueryable<Scenario> query, PagedScenarioResultRequestDto input)
        {
            return query.OrderBy(x => x.Name);
        }

        protected override Scenario MapToEntity(CreateScenarioDto createInput)
        {
            return ObjectMapper.Map<Scenario>(createInput);
        }

        protected override void MapToEntity(ScenarioDto input, Scenario entity)
        {
            ObjectMapper.Map(input, entity);
        }

        protected override ScenarioDto MapToEntityDto(Scenario entity)
        {
            var scenarioDto = base.MapToEntityDto(entity);
            if (entity.Feature != null)
            {
                scenarioDto.FeatureName = entity.Feature.Name;
            }
            return scenarioDto;
        }
    }
} 