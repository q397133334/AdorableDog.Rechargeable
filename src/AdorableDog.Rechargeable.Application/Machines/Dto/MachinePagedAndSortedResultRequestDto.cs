using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AdorableDog.Rechargeable.Machines.Dto
{
    public class MachinePagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid ProductId { get; set; } = Guid.Empty;

        public string Key { get; set; }
    }
}
