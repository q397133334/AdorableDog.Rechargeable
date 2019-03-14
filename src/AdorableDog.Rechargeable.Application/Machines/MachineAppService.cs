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
                    machine.ExpireTime = DateTime.Now.AddDays(-1);
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
                        await _repositoryMachines.UpdateAsync(machine);
                    }
                    return (machine.ExpireTime - DateTime.Now).Days;
                }
            }
            else
            {
                return -2;
            }
        }
        [Authorize]
        public async Task<PagedResultDto<MachineDto>> GetMachines(PagedAndSortedResultRequestDto inputDto)
        {
            var machines = _repositoryMachines.Where(q => q.UserId == CurrentUser.Id).Skip(inputDto.SkipCount).Take(inputDto.MaxResultCount).ToList();

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
            return new PagedResultDto<MachineDto>() { Items = query.ToList(), TotalCount = _repositoryMachines.Where(q => q.UserId == CurrentUser.Id).Count() };
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
            if(machine.ExpireTime<DateTime.Now)
            {
                machine.ExpireTime = DateTime.Now;
            }
            machine.ExpireTime = machine.ExpireTime.AddDays(serialNumber.Expire);
            machine.MachineRecords.Add(new MachineRecord()
            {
                MachineId = machine.Id,
                Desc = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}使用【{machineRechargeDto.SerialNumberId}】序列号进行充值，增加时间【{serialNumber.Expire}】天,有效期到：{ machine.ExpireTime.ToString("yyyy-MM-dd ")}",
            });

            await _repositoryMachines.UpdateAsync(machine);
            await _repositorySerialNumbers.UpdateAsync(serialNumber);
        }
    }
}
