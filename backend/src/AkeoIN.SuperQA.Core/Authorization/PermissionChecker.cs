using Abp.Authorization;
using AkeoIN.SuperQA.Authorization.Roles;
using AkeoIN.SuperQA.Authorization.Users;

namespace AkeoIN.SuperQA.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
