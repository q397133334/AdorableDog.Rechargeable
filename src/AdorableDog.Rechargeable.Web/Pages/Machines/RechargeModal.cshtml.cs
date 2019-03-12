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

namespace AdorableDog.Rechargeable.Web.Pages.Machines
{
    public class RechargeModalModel : RechargeablePageModelBase
    {
        [BindProperty]
        public MachineRechargeDto MachineRechargeDto { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid MachineId { get; set; }

        private readonly MachineAppService _machineAppService;

        public RechargeModalModel(MachineAppService machineAppService)
        {
            _machineAppService = machineAppService;
        }

        public void OnGet()
        {
            MachineRechargeDto = new MachineRechargeDto();
            MachineRechargeDto.MachineId = MachineId;
        }

        [UnitOfWork]
        public async Task<IActionResult> OnPostAsync()
        {
            await _machineAppService.Recharge(MachineRechargeDto);
            return NoContent();
        }
    }
}