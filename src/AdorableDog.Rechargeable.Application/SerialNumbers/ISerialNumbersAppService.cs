using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using AdorableDog.Rechargeable.SerialNumbers.Dto;

namespace AdorableDog.Rechargeable.SerialNumbers
{
    public interface ISerialNumbersAppService:IApplicationService
    {
        Task Create(CreateSerialNumberDto inputDto);
          
    }
}
