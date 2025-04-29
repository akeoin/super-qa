using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AkeoIN.SuperQA.ProductFeature.Dto;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Abp.Runtime.Caching;

namespace AkeoIN.SuperQA.ProductFeature
{
    [AbpAuthorize]
    public class FeatureAppService : AsyncCrudAppService<Feature, FeatureDto, int, PagedFeatureResultRequestDto, CreateFeatureDto, FeatureDto>, IFeatureAppService
    {
        public ILogger Logger { get; set; }
        private readonly ICacheManager _cacheManager;
        private const string FeatureCacheKey = "Feature:";
        private const int DefaultCacheTime = 60; // 60 minutes

        public FeatureAppService(IRepository<Feature, int> repository, ICacheManager cacheManager)
            : base(repository)
        {
            Logger = NullLogger.Instance;
            _cacheManager = cacheManager;
        }

        public override async Task<FeatureDto> GetAsync(EntityDto<int> input)
        {
            try
            {
                Logger.Debug($"Getting feature by ID: {input.Id}");
                var cacheKey = $"{FeatureCacheKey}{input.Id}";
                Logger.Info("➡ Trying to get feature from Redis cache");

                return await _cacheManager.GetCache<string, FeatureDto>(cacheKey).GetAsync(
                    cacheKey,
                    async () =>
                    {
                        var feature = await Repository.GetAllIncluding(x => x.ParentFeature)
                            .FirstOrDefaultAsync(x => x.Id == input.Id);

                        Logger.Info("❗ Cache miss. Getting  from DB 5");
                        if (feature == null)
                        {
                            throw new EntityNotFoundException(typeof(Feature), input.Id);
                        }

                        return MapToEntityDto(feature);
                    }
                );
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get feature by ID: {input.Id}", ex);
                throw;
            }
        }

        public override async Task<PagedResultDto<FeatureDto>> GetAllAsync(PagedFeatureResultRequestDto input)
        {
            try
            {
                Logger.Debug($"Getting all features with keyword: {input.Keyword}, status: {input.Status}");
                var cacheKey = $"Features:List:{input.SkipCount}:{input.MaxResultCount}:{input.Keyword}:{input.Status}:{input.ParentFeatureId}";
                Logger.Info("➡ Trying to get from Redis cache");

                return await _cacheManager.GetCache<string, PagedResultDto<FeatureDto>>(cacheKey).GetAsync(
                    cacheKey,
                    async () =>
                    {
                        var query = CreateFilteredQuery(input);
                        var totalCount = await query.CountAsync();

                        Logger.Info("❗ Cache miss. Getting from DB");

                        var items = await query
                            .Skip(input.SkipCount)
                            .Take(input.MaxResultCount)
                            .ToListAsync();

                        return new PagedResultDto<FeatureDto>(
                            totalCount,
                            items.Select(MapToEntityDto).ToList()
                        );
                    }
                );
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get all features", ex);
                throw;
            }
        }

        public override async Task<FeatureDto> CreateAsync(CreateFeatureDto input)
        {
            try
            {
                Logger.Info($"Creating feature: {input.Name}");
                Logger.Info("➡ Trying to get from Redis cache");

                var result = await base.CreateAsync(input);
                
                
                // Clear the cache after creating a new feature
                await _cacheManager.GetCache(FeatureCacheKey).ClearAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to create feature: {input.Name}", ex);
                throw;
            }
        }

        public override async Task<FeatureDto> UpdateAsync(FeatureDto input)
        {
            try
            {
                Logger.Info($"Updating feature: {input.Name}");
                var result = await base.UpdateAsync(input);
                Logger.Info($"Successfully updated feature: {input.Name}");
                
                // Clear the cache after updating a feature
                await _cacheManager.GetCache(FeatureCacheKey).ClearAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to update feature: {input.Name}", ex);
                throw;
            }
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            try
            {
                var feature = await GetEntityByIdAsync(input.Id);
                Logger.Info($"Deleting feature: {feature.Name}");
                await base.DeleteAsync(input);
                Logger.Info($"Successfully deleted feature: {feature.Name}");
                
                // Clear the cache after deleting a feature
                await _cacheManager.GetCache(FeatureCacheKey).ClearAsync();
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to delete feature with ID: {input.Id}", ex);
                throw;
            }
        }

        protected override IQueryable<Feature> CreateFilteredQuery(PagedFeatureResultRequestDto input)
        {
            try
            {
                Logger.Debug($"Filtering features with keyword: {input.Keyword}, status: {input.Status}");
                var query = Repository.GetAllIncluding(x => x.ParentFeature)
                    .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword))
                    .WhereIf(!input.Status.IsNullOrWhiteSpace(), x => x.Status == input.Status)
                    .WhereIf(input.ParentFeatureId.HasValue, x => x.ParentFeatureId == input.ParentFeatureId);
                return query;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to filter features", ex);
                throw;
            }
        }

        protected override async Task<Feature> GetEntityByIdAsync(int id)
        {
            try
            {
                Logger.Debug($"Getting feature by ID: {id}");
                var feature = await Repository.GetAllIncluding(x => x.ParentFeature)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (feature == null)
                {
                    throw new EntityNotFoundException(typeof(Feature), id);
                }

                return feature;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get feature by ID: {id}", ex);
                throw;
            }
        }

        protected override IQueryable<Feature> ApplySorting(IQueryable<Feature> query, PagedFeatureResultRequestDto input)
        {
            return query.OrderBy(x => x.Name);
        }

        protected override Feature MapToEntity(CreateFeatureDto createInput)
        {
            return ObjectMapper.Map<Feature>(createInput);
        }

        protected override void MapToEntity(FeatureDto input, Feature entity)
        {
            ObjectMapper.Map(input, entity);
        }

        protected override FeatureDto MapToEntityDto(Feature entity)
        {
            var featureDto = base.MapToEntityDto(entity);
            if (entity.ParentFeature != null)
            {
                featureDto.ParentFeatureName = entity.ParentFeature.Name;
            }
            return featureDto;
        }
    }
} 