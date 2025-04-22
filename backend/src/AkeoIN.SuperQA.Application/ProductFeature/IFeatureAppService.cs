using System;
using Abp.Application.Services;
using AkeoIN.SuperQA.ProductFeature.Dto;

namespace AkeoIN.SuperQA.ProductFeature
{
    public interface IFeatureAppService : IAsyncCrudAppService<FeatureDto, int, PagedFeatureResultRequestDto, CreateFeatureDto, FeatureDto>
    {
    }
} 