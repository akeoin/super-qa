using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using AkeoIN.SuperQA.Features.Dto;
using AutoMapper.Internal.Mappers;

namespace AkeoIN.SuperQA.Features
{
    // Application/Features/FeatureAppService.cs
    public class FeatureAppService :
    AsyncCrudAppService<Feature, FeatureDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateFeatureDto, CreateUpdateFeatureDto>,
    IFeatureAppService

    {
        public FeatureAppService(IRepository<Feature, Guid> repository) : base(repository)
        {
            LocalizationSourceName = SuperQAConsts.LocalizationSourceName;// SuperQaConsts.LocalizationSourceName;
        }

        public override Task<FeatureDto> CreateAsync(CreateUpdateFeatureDto input)
        {
            // Optionally check for edition-based limits here
            return base.CreateAsync(input);
        }

        public async Task<ListResultDto<FeatureDto>> GetAllFlatAsync()
        {
            var features = await Repository.GetAllListAsync();
            return new ListResultDto<FeatureDto>(ObjectMapper.Map<List<FeatureDto>>(features));
        }
    }

}
