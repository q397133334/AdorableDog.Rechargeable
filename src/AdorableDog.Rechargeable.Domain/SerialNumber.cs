﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AdorableDog.Rechargeable
{
    /// <summary>
    /// 序列号信息
    /// </summary>
    [Table("SerialNumbers")]
    public class SerialNumber : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [Display(Name = "订单编号")]
        public Guid? OrderId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Display(Name = "商品编号")]
        public Guid? ProductId { get; set; }

        /// <summary>
        /// 商品价格ID
        /// </summary>
        [Display(Name = "商品价格编号")]
        public Guid? ProductPriceId { get; set; }

        /// <summary>
        /// 机器ID
        /// </summary>
        [Display(Name = "机器编号")]
        public Guid? MachineId { get; set; }

        /// <summary>
        /// 销售用户ID
        /// </summary>
        [Display(Name = "销售用户")]
        public Guid? SaleUserId { get; set; }

        /// <summary>
        /// 购买用户ID
        /// </summary>
        [Display(Name = "购买用户")]
        public Guid BuyUserId { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        [Display(Name = "有效时间")]
        public int Expire { get; set; }

        [Display(Name = "有效时间")]
        public DateTime? ExpireTime { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        [Display(Name = "使用时间")]
        public DateTime? UseTime { get; set; }

        public string CreateDesc { get; set; }
    }
}
