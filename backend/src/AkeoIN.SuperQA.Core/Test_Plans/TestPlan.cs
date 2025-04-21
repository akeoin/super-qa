using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.TestRuns;
using AkeoIN.SuperQA.Features;

namespace AkeoIN.SuperQA.Test_Plans
{
    [Table("sqa.models.TestPlans")]
    public class TestPlan : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public ICollection<Feature> Features { get; set; }
    }


}
