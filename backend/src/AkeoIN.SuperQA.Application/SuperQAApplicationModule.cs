using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AkeoIN.SuperQA.Authorization;

namespace AkeoIN.SuperQA
{
    [DependsOn(
        typeof(SuperQACoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SuperQAApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SuperQAAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SuperQAApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
