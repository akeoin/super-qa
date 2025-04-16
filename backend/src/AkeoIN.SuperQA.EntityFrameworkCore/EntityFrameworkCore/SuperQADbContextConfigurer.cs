using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AkeoIN.SuperQA.EntityFrameworkCore
{
    public static class SuperQADbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SuperQADbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public static void Configure(DbContextOptionsBuilder<SuperQADbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);
            builder.UseMySql(connection, ServerVersion.AutoDetect(connection.ConnectionString));
        }
    }
}
