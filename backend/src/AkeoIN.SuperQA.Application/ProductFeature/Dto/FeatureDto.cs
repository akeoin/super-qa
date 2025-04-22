using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace AkeoIN.SuperQA.ProductFeature.Dto
{
    public class FeatureDto : EntityDto<int>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public int? ParentFeatureId { get; set; }
        
        public string ParentFeatureName { get; set; }
    }
} 