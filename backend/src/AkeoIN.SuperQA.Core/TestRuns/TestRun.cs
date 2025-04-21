using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.TestResults;
using AkeoIN.SuperQA.Test_Cases;

namespace AkeoIN.SuperQA.TestRuns
{
    [Table("sqa.TestRuns")]
    public class TestRun : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid TestCaseId { get; set; }
        [ForeignKey("TestCaseIdId")]
        public TestCase TestCase { get; set; }

        public ICollection<TestResult> TestResults { get; set; }
    }


}
