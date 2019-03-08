using AdorableDog.Rechargeable.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace AdorableDog.Rechargeable
{
    [DependsOn(
        typeof(RechargeableDomainModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule)
        )]
    public class RechargeableApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PermissionOptions>(options =>
            {
                options.DefinitionProviders.Add<RechargeablePermissionDefinitionProvider>();
            });

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<RechargeableApplicationAutoMapperProfile>();
            });
        }
    }
}
