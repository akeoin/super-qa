using System;
using Abp.Application.Services.Dto;

namespace AkeoIN.SuperQA.ProductFeature.Dto
{
    public class PagedFeatureResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Status { get; set; }
        public int? ParentFeatureId { get; set; }
    }
} 