using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Volo.Abp.Application.Services;
using AdorableDog.Rechargeable.Products.Dto;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace AdorableDog.Rechargeable.Products
{
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto,
        CreateUpdateProductDto>, IProductAppService
    {
        private readonly IRepository<Product, Guid> _repositoryProduct;

        private readonly IRepository<ProductPrice, Guid> _repositoryProductPrices;

        private readonly IRepository<SerialNumber, Guid> _repositorySerialNumbers;

        public ProductAppService(IRepository<Product, Guid> repositoryProduct,
            IRepository<ProductPrice, Guid> repositoryProductPrices,
            IRepository<SerialNumber, Guid> repositorySerialNumbers) : base(repositoryProduct)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryProductPrices = repositoryProductPrices;
            _repositorySerialNumbers = repositorySerialNumbers;
        }

        [Authorize]
        public async override Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            input.UserId = CurrentUser.Id.Value;
            return await base.CreateAsync(input);
        }

        [Authorize]
        public async override Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = _repositoryProduct.Where(q => q.UserId == CurrentUser.Id);
            var totalCount = query.Count();
            var list = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            var listDto = ObjectMapper.Map<List<Product>, List<ProductDto>>(list);

            return new PagedResultDto<ProductDto>() { Items = listDto, TotalCount = totalCount };
        }
        [Authorize]
        public async Task<List<ProductPriceDto>> GetProductPrices(Guid ProductId)
        {
            var list = _repositoryProductPrices.Where(q => q.ProductId == ProductId).ToList();
            var listDto = ObjectMapper.Map<List<ProductPrice>, List<ProductPriceDto>>(list);
            return listDto;
        }
        [Authorize]
        public async Task<List<Guid>> BatchCreateProductSerialNumber(BatchCreateProductSerialNumberDto inputDto)
        {
            var product = _repositoryProduct.Where(q => q.Id == inputDto.ProductId).FirstOrDefault();
            if (product == null)
                throw new UserFriendlyException("未找到商品信息");
            if (product.UserId != CurrentUser.Id)
                throw new UserFriendlyException("不属于自己的商品，无法创建");
            var productPrice = _repositoryProductPrices.Where(q => q.Id == inputDto.ProductPriceId).FirstOrDefault();
            if (productPrice == null)
                throw new UserFriendlyException("未找到商品价格信息");
            if (product.Id != productPrice.ProductId)
                throw new UserFriendlyException("商品信息和价格信息不匹配");

            var list = new List<SerialNumber>();
            for (int i = 0; i < inputDto.Count; i++)
            {
                var sn = new SerialNumber()
                {
                    ProductId = inputDto.ProductId,
                    ProductPriceId = inputDto.ProductPriceId,
                    Expire = productPrice.Day,
                    SaleUserId = CurrentUser.Id,
                    CreateDesc = $"{CurrentUser.UserName}通过后台系统批量创建,{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}"
                };
                list.Add(await _repositorySerialNumbers.InsertAsync(sn));
            }

            return list.Select(q => q.Id).ToList();
        }
    }
}
