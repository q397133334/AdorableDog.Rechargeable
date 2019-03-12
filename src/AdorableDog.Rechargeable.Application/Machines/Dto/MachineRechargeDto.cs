using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AdorableDog.Rechargeable.Machines.Dto
{
    public class MachineRechargeDto
    {
        [Display(Name ="机器编号")]
        public Guid MachineId { get; set; }

        [Display(Name = "序列号")]
        public Guid SerialNumberId { get; set; }
    }
}
