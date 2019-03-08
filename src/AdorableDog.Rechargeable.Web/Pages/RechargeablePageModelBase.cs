using AdorableDog.Rechargeable.Localization.Rechargeable;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AdorableDog.Rechargeable.Pages
{
    public abstract class RechargeablePageModelBase : AbpPageModel
    {
        protected RechargeablePageModelBase()
        {
            LocalizationResourceType = typeof(RechargeableResource);
        }
    }
}