using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Test_Cases;
using AkeoIN.SuperQA.ProductFeature;

namespace AkeoIN.SuperQA.Scenarios
{

    [Table("sqa.Scenarios")]
    public class Scenario : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public Guid FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        public ICollection<TestCase> TestCases { get; set; }

        public Scenario()
        {
            TestCases = new HashSet<TestCase>();
        }
    }
}
