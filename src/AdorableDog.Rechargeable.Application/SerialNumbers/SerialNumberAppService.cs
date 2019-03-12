using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AdorableDog.Rechargeable.SerialNumbers.Dto;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.PermissionManagement;
using Volo.Abp;

namespace AdorableDog.Rechargeable.SerialNumbers
{
    public class SerialNumberAppService : ApplicationService, ISerialNumbersAppService
    {
        private IRepository<SerialNumber, Guid> _repositorySerialNumbers;
        private IRepository<Product, Guid> _repositoryProduct;
        private IRepository<ProductPrice, Guid> _repositoryProductPrices;

        public SerialNumberAppService(IRepository<SerialNumber, Guid> repositorySerialNumbers,
            IRepository<Product, Guid> repositoryProduct,
             IRepository<ProductPrice, Guid> repositoryProductPrices)
        {
            _repositorySerialNumbers = repositorySerialNumbers;
            _repositoryProduct = repositoryProduct;
            _repositoryProductPrices = repositoryProductPrices;
        }//


        public async Task Create(CreateSerialNumberDto inputDto)
        {
            var serialNumber = ObjectMapper.Map<CreateSerialNumberDto, SerialNumber>(inputDto);
            var product = _repositoryProduct.Where(q => q.Id == inputDto.ProductId).FirstOrDefault();
            var productPrice = _repositoryProductPrices.Where(q => q.Id == inputDto.ProductPriceId).FirstOrDefault();
            if (product == null)
                throw new UserFriendlyException("未找到对应的商品信息");
            if (productPrice == null)
                throw new UserFriendlyException("未找到对应的价格信息");
            if (product.Id != productPrice.ProductId)
                throw new UserFriendlyException("商品信息和价格信息不匹配");

            serialNumber.SaleUserId = CurrentUser.Id;
            serialNumber.Expire = productPrice.Day;
            serialNumber.UseTime = null;

            await _repositorySerialNumbers.InsertAsync(serialNumber);

        }
    }
}
