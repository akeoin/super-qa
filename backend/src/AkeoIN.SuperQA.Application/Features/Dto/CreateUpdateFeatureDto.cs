using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace AkeoIN.SuperQA.Features.Dto
{
    public class CreateUpdateFeatureDto : EntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? ParentId { get; set; }
    }

}
