using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Data.Common;

namespace AbpPractice.EntityFrameworkCore;

public static class AbpPracticeDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<AbpPracticeDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<AbpPracticeDbContext> builder, DbConnection connection)
    {
        builder.UseNpgsql(connection);
    }
}
