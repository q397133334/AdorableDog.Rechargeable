using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace AdorableDog.Rechargeable.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class RechargeableDbContext : AbpDbContext<RechargeableDbContext>
    {
        public RechargeableDbContext(DbContextOptions<RechargeableDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureBackgroundJobs();
            modelBuilder.ConfigureAuditLogging();
        }
    }
}
