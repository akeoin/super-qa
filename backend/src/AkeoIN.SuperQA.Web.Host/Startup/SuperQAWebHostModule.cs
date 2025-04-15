using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AkeoIN.SuperQA.Configuration;

namespace AkeoIN.SuperQA.Web.Host.Startup
{
    [DependsOn(
       typeof(SuperQAWebCoreModule))]
    public class SuperQAWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SuperQAWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SuperQAWebHostModule).GetAssembly());
        }
    }
}
