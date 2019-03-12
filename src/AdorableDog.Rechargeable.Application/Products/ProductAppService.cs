using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using AdorableDog.Rechargeable.Products.Dto;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AdorableDog.Rechargeable.Products
{
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto,
        CreateUpdateProductDto>, IProductAppService
    {
        public ProductAppService(IRepository<Product, Guid> repositoryProduct) : base(repositoryProduct)
        {
        }
    }
}
