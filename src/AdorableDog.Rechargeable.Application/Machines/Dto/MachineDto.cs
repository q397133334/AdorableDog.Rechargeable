using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AdorableDog.Rechargeable.Machines.Dto
{
    public class MachineDto : EntityDto<Guid>
    {
        /// <summary>
        /// 机器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机器介绍
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 机器唯一编码
        /// </summary>
        public string DriveId { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 最后一次在线时间
        /// </summary>
        public DateTime LastOnlineTime { get; set; }

        /// <summary>
        /// 所属商品名称
        /// </summary>
        public string ProductName { get; set; }


    }
}
