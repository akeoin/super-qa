using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace AkeoIN.SuperQA.Authorization
{
    public class SuperQAAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            // Scenario permissions
            var scenarios = context.CreatePermission(PermissionNames.Pages_Scenarios, L("Scenarios"));
            scenarios.CreateChildPermission(PermissionNames.Pages_Scenarios_Create, L("CreateScenario"));
            scenarios.CreateChildPermission(PermissionNames.Pages_Scenarios_Edit, L("EditScenario"));
            scenarios.CreateChildPermission(PermissionNames.Pages_Scenarios_Delete, L("DeleteScenario"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SuperQAConsts.LocalizationSourceName);
        }
    }
}
