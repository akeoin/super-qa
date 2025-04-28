using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using AkeoIN.SuperQA.Authorization.Roles;
using AkeoIN.SuperQA.Authorization.Users;
using AkeoIN.SuperQA.Configuration;
using AkeoIN.SuperQA.Localization;
using AkeoIN.SuperQA.MultiTenancy;
using AkeoIN.SuperQA.Timing;
using AkeoIN.SuperQA.ProductFeature;
using Abp;
using AkeoIN.SuperQA.Scenarios;

namespace AkeoIN.SuperQA
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class SuperQACoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            SuperQALocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = SuperQAConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = SuperQAConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = SuperQAConsts.DefaultPassPhrase;

            // Enable entity history tracking for Feature entity
            Configuration.EntityHistory.Selectors.Add(
                new NamedTypeSelector(
                    "Feature",
                    type => type == typeof(Feature)
                )
            );

            Configuration.EntityHistory.Selectors.Add(
                new NamedTypeSelector(
                    "Scenario",
                    type => type == typeof(Scenario)
                )
            );

            // Enable entity history tracking for Tenant entity
            Configuration.EntityHistory.Selectors.Add(
                new NamedTypeSelector(
                    "Tenant",
                    type => type == typeof(Tenant)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SuperQACoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
