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

namespace AkeoIN.SuperQA.ProductFeature
{
    [AbpAuthorize]
    public class FeatureAppService : AsyncCrudAppService<Feature, FeatureDto, int, PagedFeatureResultRequestDto, CreateFeatureDto, FeatureDto>, IFeatureAppService
    {
        public ILogger Logger { get; set; }

        public FeatureAppService(IRepository<Feature, int> repository)
            : base(repository)
        {
            Logger = NullLogger.Instance;
        }

        public override async Task<FeatureDto> CreateAsync(CreateFeatureDto input)
        {
            try
            {
                Logger.Info($"Creating feature: {input.Name}");
                var result = await base.CreateAsync(input);
                Logger.Info($"Successfully created feature: {input.Name}");
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