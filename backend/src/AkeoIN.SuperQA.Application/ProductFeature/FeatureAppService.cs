using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AkeoIN.SuperQA.ProductFeature.Dto;
using Microsoft.EntityFrameworkCore;

namespace AkeoIN.SuperQA.ProductFeature
{
    [AbpAuthorize]
    public class FeatureAppService : AsyncCrudAppService<Feature, FeatureDto, int, PagedFeatureResultRequestDto, CreateFeatureDto, FeatureDto>, IFeatureAppService
    {
        public FeatureAppService(IRepository<Feature, int> repository)
            : base(repository)
        {
        }

        protected override IQueryable<Feature> CreateFilteredQuery(PagedFeatureResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.ParentFeature)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword))
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), x => x.Status == input.Status)
                .WhereIf(input.ParentFeatureId.HasValue, x => x.ParentFeatureId == input.ParentFeatureId);
        }

        protected override async Task<Feature> GetEntityByIdAsync(int id)
        {
            var feature = await Repository.GetAllIncluding(x => x.ParentFeature)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (feature == null)
            {
                throw new EntityNotFoundException(typeof(Feature), id);
            }

            return feature;
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