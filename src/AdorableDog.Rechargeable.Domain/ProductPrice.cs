using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace AdorableDog.Rechargeable
{
    /// <summary>
    /// 商品价格
    /// </summary>
    [Table("ProductPrices")]
    public class ProductPrice : Entity<Guid>
    {

        public ProductPrice() { }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Display(Name = "商品ID")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "价格")]
        public decimal Money { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [Display(Name = "有效期")]
        public int Day { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        public string Desc { get; set; }

        public virtual Product Product { get; set; }
    }
}
