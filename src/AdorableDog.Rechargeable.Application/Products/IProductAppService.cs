using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using AdorableDog.Rechargeable.Products.Dto;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;

namespace AdorableDog.Rechargeable.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto,
        CreateUpdateProductDto>
    {
        Task<List<ProductPriceDto>> GetProductPrices(Guid ProductId);

        /// <summary>
        /// 批量创建序列号
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<List<Guid>> BatchCreateProductSerialNumber(BatchCreateProductSerialNumberDto inputDto);
    }
}
 