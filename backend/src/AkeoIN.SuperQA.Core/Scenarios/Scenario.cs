using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Features;
using AkeoIN.SuperQA.Test_Cases;

namespace AkeoIN.SuperQA.Scenarios
{

    [Table("sqa.models.Scenarios")]
    public class Scenario : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        public ICollection<TestCase> TestCases { get; set; }
    }
}
