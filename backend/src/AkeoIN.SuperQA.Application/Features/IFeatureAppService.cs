using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Application.Services;
using AkeoIN.SuperQA.Features.Dto;

namespace AkeoIN.SuperQA.Features
{
    public interface IFeatureAppService : IAsyncCrudAppService<FeatureDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateFeatureDto, CreateUpdateFeatureDto>
    {
        Task<ListResultDto<FeatureDto>> GetAllFlatAsync();
    }

}
