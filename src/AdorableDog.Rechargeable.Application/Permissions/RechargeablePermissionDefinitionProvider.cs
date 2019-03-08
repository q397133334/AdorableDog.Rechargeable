using AdorableDog.Rechargeable.Localization.Rechargeable;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AdorableDog.Rechargeable.Permissions
{
    public class RechargeablePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(RechargeablePermissions.GroupName);

            //Define your own permissions here. Examaple:
            //myGroup.AddPermission(RechargeablePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<RechargeableResource>(name);
        }
    }
}
