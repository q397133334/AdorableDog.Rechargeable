using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using AdorableDog.Rechargeable.Localization.Rechargeable;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AdorableDog.Rechargeable.Pages
{
    public abstract class RechargeablePageBase : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<RechargeableResource> L { get; set; }
    }
}
