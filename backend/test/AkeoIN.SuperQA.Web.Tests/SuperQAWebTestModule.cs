using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AkeoIN.SuperQA.EntityFrameworkCore;
using AkeoIN.SuperQA.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AkeoIN.SuperQA.Web.Tests
{
    [DependsOn(
        typeof(SuperQAWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class SuperQAWebTestModule : AbpModule
    {
        public SuperQAWebTestModule(SuperQAEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SuperQAWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(SuperQAWebMvcModule).Assembly);
        }
    }
}