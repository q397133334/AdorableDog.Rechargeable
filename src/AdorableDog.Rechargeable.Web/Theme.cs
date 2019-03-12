using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace AdorableDog.Rechargeable
{
    [ThemeName(Name)]
    public class Theme : BasicTheme, ITheme, ITransientDependency
    {
        public new const string Name = "Rechargeable";

        public new string GetLayout(string name, bool fallbackToDefault = true)
        {
            switch (name)
            {
                case StandardLayouts.Application:
                    return "~/Views/Layouts/Application.cshtml";
            }
            return base.GetLayout(name, fallbackToDefault);
        }
    }
}
