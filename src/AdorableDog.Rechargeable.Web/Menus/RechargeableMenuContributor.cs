using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using AdorableDog.Rechargeable.Localization.Rechargeable;
using Volo.Abp.UI.Navigation;

namespace AdorableDog.Rechargeable.Menus
{
    public class RechargeableMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<RechargeableResource>>();

            context.Menu.Items.Insert(0, new ApplicationMenuItem("Rechargeable.Home", l["Menu:Home"], "/"));
        }
    }
}
