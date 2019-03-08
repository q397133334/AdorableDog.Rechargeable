using AdorableDog.Rechargeable.Localization.Rechargeable;
using AdorableDog.Rechargeable.Settings;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Localization.Resources.AbpValidation;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.Settings;
using Volo.Abp.VirtualFileSystem;

namespace AdorableDog.Rechargeable
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpAuditingModule),
        typeof(BackgroundJobsDomainModule),
        typeof(AbpAuditLoggingDomainModule)
        )]
    public class RechargeableDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<VirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RechargeableDomainModule>("AdorableDog.Rechargeable");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<RechargeableResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Rechargeable");
            });

            Configure<SettingOptions>(options =>
            {
                options.DefinitionProviders.Add<RechargeableSettingDefinitionProvider>();
            });
        }
    }
}
