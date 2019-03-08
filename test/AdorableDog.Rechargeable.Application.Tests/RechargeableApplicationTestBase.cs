using Volo.Abp;

namespace AdorableDog.Rechargeable
{
    public abstract class RechargeableApplicationTestBase : AbpIntegratedTest<RechargeableApplicationTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
