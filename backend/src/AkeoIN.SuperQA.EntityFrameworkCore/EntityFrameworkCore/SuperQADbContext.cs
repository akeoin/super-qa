using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AkeoIN.SuperQA.Authorization.Roles;
using AkeoIN.SuperQA.Authorization.Users;
using AkeoIN.SuperQA.MultiTenancy;
using AkeoIN.SuperQA.Scenarios;
using AkeoIN.SuperQA.Test_Cases;
using AkeoIN.SuperQA.Test_Plans;
using AkeoIN.SuperQA.TestResults;
using AkeoIN.SuperQA.TestRuns;
using AkeoIN.SuperQA.ProductFeature;

namespace AkeoIN.SuperQA.EntityFrameworkCore
{
    public class SuperQADbContext : AbpZeroDbContext<Tenant, Role, User, SuperQADbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Feature> Features { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestPlan> TestPlans { get; set; }
        public DbSet<TestRun> TestRuns { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        public SuperQADbContext(DbContextOptions<SuperQADbContext> options)
            : base(options)
        {
        }     
    }
}
