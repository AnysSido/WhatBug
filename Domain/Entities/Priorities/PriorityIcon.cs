using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Entities.Priorities
{
    public class PriorityIcon
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Priority> Priorities { get; set; }
    }
}
