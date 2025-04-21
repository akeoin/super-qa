using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Scenarios;
using AkeoIN.SuperQA.Test_Plans;

namespace AkeoIN.SuperQA.Features
{

    [Table("sqa.models.Features")]
    public class Feature : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid? ParentFeatureId { get; set; }
        [ForeignKey("ParentFeatureId")]
        public Feature ParentFeature { get; set; }

        public Guid TestPlanId { get; set; }
        [ForeignKey("TestPlanId")]
        public TestPlan TestPlan { get; set; }
        public ICollection<Feature> ChildFeatures { get; set; }
        public ICollection<Scenario> Scenarios { get; set; }
    }


}
