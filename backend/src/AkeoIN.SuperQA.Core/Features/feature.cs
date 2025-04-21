using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace AkeoIN.SuperQA.Features
{
    // Domain/Features/Feature.cs
    public class Feature : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Feature Parent { get; set; }

        public virtual ICollection<Feature> Children { get; set; }

        public string Description { get; set; }
    }


}
