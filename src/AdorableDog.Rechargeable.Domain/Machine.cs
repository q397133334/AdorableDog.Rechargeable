using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AdorableDog.Rechargeable
{
    /// <summary>
    /// 机器信息
    /// </summary>
    [Table("Machines")]
    public class Machine : FullAuditedEntity<Guid>
    {
        public Machine()
        {
            MachineRecords = new List<MachineRecord>();
        }

        /// <summary>
        /// 机器名称
        /// </summary>
        [Display(Name = "机器名称")]
        public string Name { get; set; }

        /// <summary>
        /// 机器介绍
        /// </summary>
        [Display(Name = "机器详情")]
        public string Desc { get; set; }

        /// <summary>
        /// 机器唯一编码
        /// </summary>
        [Display(Name = "机器编码")]
        public string DriveId { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [Display(Name = "到期时间")]
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 最后一次在线时间
        /// </summary>
        [Display(Name = "最后一次在线时间")]
        public DateTime LastOnlineTime { get; set; }

        [Display(Name = "自动续费")]
        [DefaultValue(true)]
        public bool AutoRecharge { get; set; } = true;

        /// <summary>
        /// 所属用户
        /// </summary>
        [Display(Name = "用户")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 游戏编码
        /// </summary>
        [Display(Name = "游戏")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Volo.Abp.Identity.IdentityUser User { get; set; }

        public virtual ICollection<MachineRecord> MachineRecords { get; set; }
    }



}
