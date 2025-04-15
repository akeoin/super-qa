using Abp.MultiTenancy;
using AkeoIN.SuperQA.Authorization.Users;

namespace AkeoIN.SuperQA.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
