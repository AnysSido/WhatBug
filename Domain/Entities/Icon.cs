using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Domain.Entities
{
    public class Icon
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Priority> Priorities { get; set; }
    }
}
