using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace AkeoIN.SuperQA.Features.Dto
{
    // Application/Features/Dto/FeatureDto.cs
    [AutoMapFrom(typeof(Feature))]
    public class FeatureDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }

}
