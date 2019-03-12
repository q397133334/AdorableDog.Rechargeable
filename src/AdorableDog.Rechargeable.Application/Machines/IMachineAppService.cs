using AdorableDog.Rechargeable.Machines.Dto;
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
        Task<PagedResultDto<MachineDto>> GetMachines(PagedAndSortedResultRequestDto inputDto);

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="machineRechargeDto"></param>
        /// <returns></returns>
        Task Recharge(MachineRechargeDto machineRechargeDto);
    }
}
