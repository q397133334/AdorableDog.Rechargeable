using AdorableDog.Rechargeable.Machines.Dto;
using AdorableDog.Rechargeable.Products.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AdorableDog.Rechargeable.Machines
{
    public interface IMachineAppService : IApplicationService
    {
        /// <summary>
        /// 验证机器时间
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<int> GetMachineExpireTime(MachineExpireTimeDto inputDto);

        /// <summary>
        /// 获取机器列表
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<PagedResultDto<MachineDto>> GetMachines(MachinePagedAndSortedResultRequestDto inputDto);

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="machineRechargeDto"></param>
        /// <returns></returns>
        Task Recharge(MachineRechargeDto machineRechargeDto);

        /// <summary>
        /// 批量充值序列号
        /// </summary>
        /// <returns></returns>
        Task<string> Recharges(List<Guid> Guids);

        /// <summary>
        /// 获取开通的商品
        /// </summary>
        /// <returns></returns>
        Task<List<ProductDto>> GetUseProduct();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(Guid id);

        /// <summary>
        /// 解绑
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
        Task<Guid> Relieve(Guid machineId);
    }
}
