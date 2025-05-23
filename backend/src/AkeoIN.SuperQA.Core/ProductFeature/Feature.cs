﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Scenarios;

namespace AkeoIN.SuperQA.ProductFeature
{
    [Table("sqa.Features")]
    public class Feature : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public int? ParentFeatureId { get; set; }
        [ForeignKey("ParentFeatureId")]
        public Feature ParentFeature { get; set; }

        public ICollection<Feature> ChildFeatures { get; set; }
        public ICollection<Scenario> Scenarios { get; set; }

        public Feature()
        {
            ChildFeatures = new HashSet<Feature>();
            Scenarios = new HashSet<Scenario>();
        }
    }
}
