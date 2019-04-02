using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AdorableDog.Rechargeable.Machines;
using AdorableDog.Rechargeable.Machines.Dto;
using AdorableDog.Rechargeable.Pages;
using Volo.Abp.Uow;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.ComponentModel.DataAnnotations;

namespace AdorableDog.Rechargeable.Web.Pages.Machines
{
    public class RechargesModalModel : RechargeablePageModelBase
    {
        [BindProperty]
        public Body Body { get; set; }
        public string Guids { get; set; }

        private readonly IMachineAppService _machineAppService;

        public RechargesModalModel(IMachineAppService machineAppService)
        {
            _machineAppService = machineAppService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var strings = Body.Guids.Split(",").ToList();
            var guids = new List<Guid>();
            foreach (var item in strings)
            {
                guids.Add(new Guid(item));
            }
            var result = new ContentResult();
            result.Content = await _machineAppService.Recharges(guids);

            return result;
        }
    }

    public class Body
    {
        [Required]
        [TextArea]
        [Display(Name = "序列号(请用,分隔开)")]
        public string Guids { get; set; }
    }
}