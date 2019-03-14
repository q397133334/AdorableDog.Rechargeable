using System;
using System.Collections.Generic;
using System.Text;

namespace AdorableDog.Rechargeable.Products.Dto
{
    /// <summary>
    /// 批量创建商品序列号DTO
    /// </summary>
    public class BatchCreateProductSerialNumberDto
    {
        public Guid ProductId { get; set; }

        public Guid ProductPriceId { get; set; }

        public int Count { get; set; }
    }
}
