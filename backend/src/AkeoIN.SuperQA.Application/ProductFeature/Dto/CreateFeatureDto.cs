using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace AkeoIN.SuperQA.ProductFeature.Dto
{
    [AutoMapTo(typeof(Feature))]
    public class CreateFeatureDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public int? ParentFeatureId { get; set; }
    }
} 