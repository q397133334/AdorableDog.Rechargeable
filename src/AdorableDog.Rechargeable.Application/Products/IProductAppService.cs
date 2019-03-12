using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using AdorableDog.Rechargeable.Products.Dto;
using Volo.Abp.Application.Dtos;

namespace AdorableDog.Rechargeable.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto,
        CreateUpdateProductDto>
    {

    }
}
