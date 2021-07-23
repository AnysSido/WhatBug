using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Entities.JoinTables
{
    public class UserPermission : AuditableEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
