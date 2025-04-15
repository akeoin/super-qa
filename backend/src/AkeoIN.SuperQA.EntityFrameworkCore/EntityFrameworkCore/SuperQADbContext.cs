using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AkeoIN.SuperQA.Authorization.Roles;
using AkeoIN.SuperQA.Authorization.Users;
using AkeoIN.SuperQA.MultiTenancy;

namespace AkeoIN.SuperQA.EntityFrameworkCore
{
    public class SuperQADbContext : AbpZeroDbContext<Tenant, Role, User, SuperQADbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public SuperQADbContext(DbContextOptions<SuperQADbContext> options)
            : base(options)
        {
        }
    }
}
