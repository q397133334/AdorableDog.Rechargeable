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
        public DbSet<Machine> Machines { get; set; }

        public DbSet<MachineRecord> MachineRecords { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<SerialNumber> SerialNumbers { get; set; }

        public DbSet<ProductPrice> ProductPrices { get; set; }

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

            modelBuilder.Entity<Machine>(b =>
            {
                b.HasMany(m => m.MachineRecords).WithOne().HasForeignKey(mr => mr.MachineId).IsRequired();

                b.HasIndex(m => m.DriveId);
                b.HasIndex(m => m.ProductId);
                b.HasIndex(m => m.UserId);
                b.HasIndex(m => new { m.ProductId, m.DriveId, m.UserId });
            });
            modelBuilder.Entity<MachineRecord>(b =>
            {
                b.HasIndex(mr => mr.MachineId);

                b.HasOne<Machine>().WithMany().HasForeignKey(m => m.MachineId).IsRequired();
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.HasMany(p => p.ProductPrices).WithOne().HasForeignKey(pp => pp.ProductId).IsRequired();
            });

            modelBuilder.Entity<ProductPrice>(b =>
            {
                b.HasIndex(pp => pp.ProductId);

                b.HasOne<Product>().WithMany().HasForeignKey(pp => pp.ProductId).IsRequired();
            });

            modelBuilder.Entity<SerialNumber>(b =>
            {
                b.HasIndex(sn => sn.ProductId);
                b.HasIndex(sn => sn.SaleUserId);
                b.HasIndex(sn => sn.MachineId);
                b.HasIndex(sn => sn.BuyUserId);
                b.HasIndex(sn => sn.ProductPriceId);
            });
        }
    }
}
