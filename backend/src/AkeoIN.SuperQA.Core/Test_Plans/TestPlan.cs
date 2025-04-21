using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Features;

namespace AkeoIN.SuperQA.Test_Plans
{
    [Table("sqa.models.TestPlans")]
    public class TestPlan : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public ICollection<Feature> Features { get; set; }

        public TestPlan()
        {
            Features = new HashSet<Feature>();
        }
    }
}
