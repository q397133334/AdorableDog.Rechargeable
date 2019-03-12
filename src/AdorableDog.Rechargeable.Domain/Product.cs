using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace AdorableDog.Rechargeable
{
    /// <summary>
    /// 商品名称
    /// </summary>
    public class Product : Entity<Guid>
    {
        public Product()
        {
            ProductPrices = new List<ProductPrice>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 游戏名称
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// 详细说明
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        public Guid UserId { get; set; }

        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
