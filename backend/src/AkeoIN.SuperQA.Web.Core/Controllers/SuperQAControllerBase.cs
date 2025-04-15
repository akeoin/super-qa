using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AkeoIN.SuperQA.Controllers
{
    public abstract class SuperQAControllerBase: AbpController
    {
        protected SuperQAControllerBase()
        {
            LocalizationSourceName = SuperQAConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
