using Volo.Abp.Settings;

namespace AdorableDog.Rechargeable.Settings
{
    public class RechargeableSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(RechargeableSettings.MySetting1));
        }
    }
}
