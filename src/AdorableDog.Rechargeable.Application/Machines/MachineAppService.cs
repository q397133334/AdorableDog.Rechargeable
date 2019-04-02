using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AdorableDog.Rechargeable.Machines.Dto;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using AdorableDog.Rechargeable.Products.Dto;

namespace AdorableDog.Rechargeable.Machines
{
    public class MachineAppService : ApplicationService, IMachineAppService
    {
        IdentityUserManager _identityUserManager;
        SignInManager<IdentityUser> _signInManager;

        private readonly IRepository<Machine, Guid> _repositoryMachines;
        private readonly IRepository<Product, Guid> _repositoryProducts;
        private readonly IRepository<SerialNumber, Guid> _repositorySerialNumbers;


        public MachineAppService(IRepository<Machine, Guid> repositoryMachines,
             IRepository<Product, Guid> repositoryProducts,
             IRepository<SerialNumber, Guid> repositorySerialNumbers,
            IdentityUserManager identityUserManager,
            SignInManager<IdentityUser> signInManager)
        {
            _repositoryMachines = repositoryMachines;
            _repositoryProducts = repositoryProducts;
            _repositorySerialNumbers = repositorySerialNumbers;
            _identityUserManager = identityUserManager;
            _signInManager = signInManager;
        }
        //http://localhost:53929/api/app/machine/machineExpireTime?UserNameOrEmailAddress=fangang&Password=123qwe&DriveId=computerfgworker&ProductId=6973da64-642d-9b10-0acf-39ec738121de
        public async Task<int> GetMachineExpireTime(MachineExpireTimeDto inputDto)
        {
            if (string.IsNullOrEmpty(inputDto.UserNameOrEmailAddress) || string.IsNullOrEmpty(inputDto.Password))
            {
                return -2;
            }
            var identityUser = await _identityUserManager.FindByNameAsync(inputDto.UserNameOrEmailAddress);
            var loginResult = await _signInManager.CheckPasswordSignInAsync(identityUser, inputDto.Password, true);
            if (loginResult.Succeeded)
            {
                //验证机器Drive 和Product
                var machine = _repositoryMachines.Where(q => q.DriveId == inputDto.DriveId && q.ProductId == inputDto.ProductId).FirstOrDefault();
                if (machine == null)
                {
                    machine = new Machine();
                    machine.Name = inputDto.Name;
                    machine.ProductId = inputDto.ProductId;
                    machine.UserId = identityUser.Id;
                    machine.DriveId = inputDto.DriveId;
                    machine.Desc = inputDto.Desc;
                    machine.LastOnlineTime = DateTime.Now;
                    machine.ExpireTime = DateTime.Now.AddMinutes(20);//赠送一小时使用期限，用来转移机器剩余使用时间
                    machine.Id = Guid.NewGuid();

                    await _repositoryMachines.InsertAsync(machine);
                    return -1;
                }
                else
                {
                    if (machine.Name != inputDto.Name)
                    {
                        machine.Name = inputDto.Name;
                        machine.Desc = inputDto.Desc;
                    }
                    machine.LastOnlineTime = DateTime.Now;

                    //获取剩余分钟数 
                    int ExpireTime = (machine.ExpireTime - DateTime.Now).Minutes;
                    //对剩余时间小于10分钟的机器进行自动充值
                    if (ExpireTime < 10 && machine.AutoRecharge)
                    {
                        //查询可用序列号,优先使用有到期时间的账号
                        var serialNumber = _repositorySerialNumbers.Where(q => q.BuyUserId == CurrentUser.Id.Value && q.ProductId == machine.ProductId && q.UseTime == null && (q.MachineId == Guid.Empty || q.MachineId == null)).OrderByDescending(q => q.ExpireTime).FirstOrDefault();
                        if (serialNumber != null)
                        {
                            serialNumber.UseTime = DateTime.Now;
                            serialNumber.MachineId = machine.Id;
                            serialNumber.CreateDesc += $"\r\n通过平台系统自动续费充值，充值机器号:{machine.Id},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                            if (machine.ExpireTime < DateTime.Now)
                            {
                                machine.ExpireTime = DateTime.Now;
                            }
                            machine.ExpireTime = machine.ExpireTime.AddDays(serialNumber.Expire);
                            machine.MachineRecords.Add(new MachineRecord()
                            {
                                MachineId = machine.Id,
                                Desc = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}使用【{serialNumber.Id}】序列号进行充值，增加时间【{serialNumber.Expire}】天,有效期到：{ machine.ExpireTime.ToString("yyyy-MM-dd ")}，该行为由系统自动充值完成",
                            });
                        }
                    }
                    await _repositoryMachines.UpdateAsync(machine);
                    ExpireTime = (machine.ExpireTime - DateTime.Now).Minutes / 60;
                    return ExpireTime;
                }
            }
            else
            {
                return -2;
            }
        }
        [Authorize]
        public async Task<PagedResultDto<MachineDto>> GetMachines(MachinePagedAndSortedResultRequestDto inputDto)
        {
            Expression<Func<Machine, bool>> fuc;
            if (await AuthorizationService.IsGrantedAsync(Permissions.RechargeablePermissions.ShowAllMachine))
            {
                fuc = q => true;
            }
            else
            {
                fuc = q => q.UserId == CurrentUser.Id;
            }
            var machines = _repositoryMachines.Where(fuc)
                .WhereIf(inputDto.ProductId != Guid.Empty, q => q.ProductId == inputDto.ProductId)
                .WhereIf(!string.IsNullOrEmpty(inputDto.Key), q => q.Name.Contains(inputDto.Key) || q.DriveId == inputDto.Key)
                .Skip(inputDto.SkipCount).Take(inputDto.MaxResultCount).ToList();

            var query = from m in machines
                        join p in _repositoryProducts
                        on m.ProductId equals p.Id
                        select new MachineDto
                        {
                            Id = m.Id,
                            DriveId = m.DriveId,
                            Desc = m.Desc,
                            ExpireTime = m.ExpireTime,
                            LastOnlineTime = m.LastOnlineTime,
                            Name = m.Name,
                            ProductName = p.Name
                        };
            return new PagedResultDto<MachineDto>()
            {
                Items = query.ToList(),
                TotalCount = _repositoryMachines.Where(fuc)
                .WhereIf(inputDto.ProductId != Guid.Empty, q => q.ProductId == inputDto.ProductId)
                .WhereIf(!string.IsNullOrEmpty(inputDto.Key), q => q.Name.Contains(inputDto.Key) || q.DriveId == inputDto.Key).Count()
            };
        }

        [Authorize]
        public async Task Recharge(MachineRechargeDto machineRechargeDto)
        {
            var machine = _repositoryMachines.Where(q => q.Id == machineRechargeDto.MachineId).FirstOrDefault();
            var serialNumber = _repositorySerialNumbers.Where(q => q.Id == machineRechargeDto.SerialNumberId).FirstOrDefault();
            if (machine == null)
                throw new UserFriendlyException("未找到机器信息");
            if (serialNumber == null)
                throw new UserFriendlyException("未找到序列号信息");
            if (serialNumber.UseTime != null)
                throw new UserFriendlyException("该序列号已被使用，如有疑问请联系管理员");
            if (machine.ProductId != serialNumber.ProductId)
                throw new UserFriendlyException("机器信息与序列号信息不匹配，无法充值");

            serialNumber.UseTime = DateTime.Now;
            serialNumber.BuyUserId = CurrentUser.Id ?? new Guid();
            serialNumber.MachineId = machine.Id;
            serialNumber.CreateDesc += $"\r\n{CurrentUser.UserName}通过平台充值，充值机器号:{machine.Id},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            if (machine.ExpireTime < DateTime.Now)
            {
                machine.ExpireTime = DateTime.Now;
            }
            if (serialNumber.Expire > 0)
            {
                machine.ExpireTime = machine.ExpireTime.AddDays(serialNumber.Expire);
            }
            else
            {
                machine.ExpireTime = serialNumber.ExpireTime.Value;
            }

            machine.MachineRecords.Add(new MachineRecord()
            {
                MachineId = machine.Id,
                Desc = $"{CurrentUser.UserName}使用【{machineRechargeDto.SerialNumberId}】序列号进行充值，增加时间【{serialNumber.Expire}】天,有效期到：{ machine.ExpireTime.ToString("yyyy-MM-dd ")}",
            });

            await _repositoryMachines.UpdateAsync(machine);
            await _repositorySerialNumbers.UpdateAsync(serialNumber);
        }
        [Authorize]
        public async Task<List<ProductDto>> GetUseProduct()
        {
            var productIds = _repositoryMachines.Where(q => q.UserId == CurrentUser.Id.Value).GroupBy(q => q.ProductId).Select(q => q.Key).ToList();
            var products = _repositoryProducts.Where(q => productIds.Contains(q.Id)).ToList();
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
        }

        [Authorize]
        public async Task<string> Recharges(List<Guid> Guids)
        {
            var serialNumbers = _repositorySerialNumbers.Where(q => Guids.Contains(q.Id) && q.UseTime == null && (q.MachineId == Guid.Empty || q.MachineId == null) && (q.BuyUserId == Guid.Empty || q.BuyUserId == null)).ToList();
            foreach (var sn in serialNumbers)
            {
                sn.BuyUserId = CurrentUser.Id.Value;
                sn.CreateDesc += $"\n\r{CurrentUser.UserName}通过批量充值使用,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                await _repositorySerialNumbers.UpdateAsync(sn);
            }
            var result = $"输入序列号{Guids.Count}条，成功验证{serialNumbers.Count}条。\n\r";
            foreach (var sn in serialNumbers)
            {
                result += $"[{sn.Id}]\n\r";
            }
            return result;
        }

        [Authorize]
        public async Task Delete(Guid id)
        {
            var machine = _repositoryMachines.Where(q => q.Id == id).FirstOrDefault();
            if (machine != null)
            {
                await _repositoryMachines.DeleteAsync(machine);
            }
            else
            {
                throw new UserFriendlyException("未找到机器信息");
            }
        }

        /// <summary>
        /// 对账号进行解绑，并生成新的序列号。新的序列号记录有效日期
        /// </summary>
        /// <param name="driveId"></param>
        /// <param name="machineId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Guid> Relieve(Guid machineId)
        {
            var machine = _repositoryMachines.Where(q => q.Id == machineId).FirstOrDefault();
            if (machine != null)
            {
                if (machine.ExpireTime > DateTime.Now)
                {
                    var sn = new SerialNumber();
                    sn.BuyUserId = machine.UserId;
                    sn.CreateDesc = $"{CurrentUser.UserName}解绑机器，生成";
                    sn.ExpireTime = machine.ExpireTime;
                    sn.ProductId = machine.ProductId;
                    sn.ProductPriceId = Guid.Empty;
                    sn.Id = Guid.NewGuid();
                    await _repositorySerialNumbers.InsertAsync(sn);
                    machine.ExpireTime = DateTime.Now.AddDays(-1);
                    machine.MachineRecords.Add(new MachineRecord()
                    {
                        MachineId = machine.Id,
                        Desc = $"{CurrentUser.UserName}对该机器进行解绑，生成的序列号：{sn.Id},有效期到{sn.ExpireTime.Value.ToString("yyyy-MM-dd HH:mm:ss")}",
                    });
                    return sn.Id;
                }
                else
                {
                    throw new UserFriendlyException("解绑成功，因时间已经到期，未生成新的返点序列号");
                }

            }
            else
            {
                throw new UserFriendlyException("未找到机器信息");
            }
        }
    }
}
