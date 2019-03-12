using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace AdorableDog.Rechargeable
{
    /// <summary>
    ///  机器记录
    /// </summary>
    [Table("MachineRecords")]
    public class MachineRecord : Entity<int>
    {
        [Display(Name = "详细情况")]
        public string Desc { get; set; }

        [Display(Name = "机器编号")]
        public Guid MachineId { get; set; }

        public virtual Machine Machine { get; set; }
    }
}
