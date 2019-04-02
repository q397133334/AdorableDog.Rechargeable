using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AdorableDog.Rechargeable.Products.Dto;
using AdorableDog.Rechargeable.Machines;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdorableDog.Rechargeable.Web.Pages.Machines
{
    public class IndexModel : PageModel
    {
        private readonly IMachineAppService _machineAppService;
        //public List<ProductDto> Products { get; set; }

        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();


        public IndexModel(IMachineAppService machineAppService)
        {
            _machineAppService = machineAppService;
        }

        public async void OnGet()
        {
            var p = await _machineAppService.GetUseProduct();
            Products.Add(new SelectListItem() { Text = "全部", Value = Guid.Empty.ToString() });
            foreach (var item in p)
            {
                Products.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
        }
    }
}