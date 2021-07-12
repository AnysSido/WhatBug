using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Domain.Entities
{
    public class PriorityScheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Priority> Priorities { get; set; }
        public List<Project> Projects { get; set; }
    }
}
