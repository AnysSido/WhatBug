using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Common
{
    public class AuditableEntity
    {
        public int CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
