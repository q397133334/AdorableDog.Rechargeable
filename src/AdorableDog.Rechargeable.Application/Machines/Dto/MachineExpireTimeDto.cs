using System;
using System.Collections.Generic;
using System.Text;

namespace AdorableDog.Rechargeable.Machines.Dto
{
    public class MachineExpireTimeDto
    {
        public string UserNameOrEmailAddress { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string DriveId { get; set; }

        public Guid ProductId { get; set; }

        public string Desc { get; set; }
    }
}
