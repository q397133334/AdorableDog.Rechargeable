﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AutoMapper;

namespace AdorableDog.Rechargeable.Products.Dto
{

    [AutoMapFrom(typeof(Product))]
    public class ProductDto : EntityDto<Guid>
    {
        public ProductDto()
        {
            ProductPrices = new List<ProductPriceDto>();
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

        public virtual List<ProductPriceDto> ProductPrices { get; set; }
    }
}
