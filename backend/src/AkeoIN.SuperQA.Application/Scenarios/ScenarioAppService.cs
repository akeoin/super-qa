using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AkeoIN.SuperQA.Scenarios.Dtos;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Abp.Runtime.Caching;

namespace AkeoIN.SuperQA.Scenarios
{
    public class ScenarioAppService : AsyncCrudAppService<Scenario, ScenarioDto, int, PagedScenarioResultRequestDto, CreateScenarioDto, ScenarioDto>, IScenarioAppService
    {
        public ILogger Logger { get; set; }
        private readonly ICacheManager _cacheManager;
        private const string ScenarioCacheKey = "Scenario:";
        private const int DefaultCacheTime = 60; // 60 minutes

        public ScenarioAppService(IRepository<Scenario, int> repository, ICacheManager cacheManager) : base(repository)
        {
            Logger = NullLogger.Instance;
            _cacheManager = cacheManager;
        }

        public override async Task<ScenarioDto> GetAsync(EntityDto<int> input)
        {
            try
            {
                Logger.Debug($"Getting scenario by ID: {input.Id}");
                var cacheKey = $"{ScenarioCacheKey}{input.Id}";
                Logger.Info("➡ Trying to get Scanario from Redis cache");

                return await _cacheManager.GetCache<string, ScenarioDto>(cacheKey).GetAsync(
                    cacheKey,
                    async () =>
                    {
                        var scenario = await Repository.GetAllIncluding(x => x.Feature)
                            .FirstOrDefaultAsync(x => x.Id == input.Id);

                        Logger.Info("❗ Cache miss. Getting Scenario from DB");
                        if (scenario == null)
                        {
                            throw new EntityNotFoundException(typeof(Scenario), input.Id);
                        }

                        return MapToEntityDto(scenario);
                    }
                );
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get scenario by ID: {input.Id}", ex);
                throw;
            }
        }

        public override async Task<PagedResultDto<ScenarioDto>> GetAllAsync(PagedScenarioResultRequestDto input)
        {
            try
            {
                Logger.Debug($"Getting all scenarios with keyword: {input.Keyword}, status: {input.Status}");
                var cacheKey = $"Scenarios:List:{input.SkipCount}:{input.MaxResultCount}:{input.Keyword}:{input.Status}:{input.FeatureId}";

                Logger.Info("➡ Trying to get Scanarios from Redis cache");
                return await _cacheManager.GetCache<string, PagedResultDto<ScenarioDto>>(cacheKey).GetAsync(
                    cacheKey,
                    async () =>
                    {
                        var query = CreateFilteredQuery(input);
                        var totalCount = await query.CountAsync();
                        var items = await query
                            .Skip(input.SkipCount)
                            .Take(input.MaxResultCount)
                            .ToListAsync();

                        Logger.Info("❗ Cache miss. Getting Scenarios from DB");
                        return new PagedResultDto<ScenarioDto>(
                            totalCount,
                            items.Select(MapToEntityDto).ToList()
                        );
                    }
                );
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get all scenarios", ex);
                throw;
            }
        }

        public override async Task<ScenarioDto> CreateAsync(CreateScenarioDto input)
        {
            try
            {
                Logger.Info($"Creating scenario: {input.Name}");
                var result = await base.CreateAsync(input);
                Logger.Info($"Successfully created scenario: {input.Name}");
                
                // Clear the cache after creating a new scenario
                await _cacheManager.GetCache(ScenarioCacheKey).ClearAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to create scenario: {input.Name}", ex);
                throw;
            }
        }

        public override async Task<ScenarioDto> UpdateAsync(ScenarioDto input)
        {
            try
            {
                Logger.Info($"Updating scenario: {input.Name}");
                var result = await base.UpdateAsync(input);
                Logger.Info($"Successfully updated scenario: {input.Name}");
                
                // Clear the cache after updating a scenario
                await _cacheManager.GetCache(ScenarioCacheKey).ClearAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to update scenario: {input.Name}", ex);
                throw;
            }
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            try
            {
                var scenario = await GetEntityByIdAsync(input.Id);
                Logger.Info($"Deleting scenario: {scenario.Name}");
                await base.DeleteAsync(input);
                Logger.Info($"Successfully deleted scenario: {scenario.Name}");
                
                // Clear the cache after deleting a scenario
                await _cacheManager.GetCache(ScenarioCacheKey).ClearAsync();
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to delete scenario with ID: {input.Id}", ex);
                throw;
            }
        }

        protected override IQueryable<Scenario> CreateFilteredQuery(PagedScenarioResultRequestDto input)
        {
            try
            {
                Logger.Debug($"Filtering scenarios with keyword: {input.Keyword}, status: {input.Status}");
                var query = Repository.GetAllIncluding(x => x.Feature)
                    .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword))
                    .WhereIf(!input.Status.IsNullOrWhiteSpace(), x => x.Status == input.Status)
                    .WhereIf(input.FeatureId.HasValue, x => x.FeatureId == input.FeatureId.Value);
                return query;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to filter scenarios", ex);
                throw;
            }
        }

        protected override async Task<Scenario> GetEntityByIdAsync(int id)
        {
            try
            {
                Logger.Debug($"Getting scenario by ID: {id}");
                var scenario = await Repository.GetAllIncluding(x => x.Feature)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (scenario == null)
                {
                    throw new EntityNotFoundException(typeof(Scenario), id);
                }

                return scenario;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get scenario by ID: {id}", ex);
                throw;
            }
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