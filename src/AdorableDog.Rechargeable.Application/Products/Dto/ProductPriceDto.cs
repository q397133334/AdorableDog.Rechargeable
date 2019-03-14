using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AutoMapper;
using AdorableDog.Rechargeable;

namespace AdorableDog.Rechargeable.Products.Dto
{

    [AutoMapTo(typeof(ProductPrice))]
    [AutoMapFrom(typeof(ProductPrice))]
    public class ProductPriceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Desc { get; set; }

        public Guid ProductId { get; set; }
    }
}
