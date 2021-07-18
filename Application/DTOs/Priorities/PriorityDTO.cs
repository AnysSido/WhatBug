using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Common;

namespace WhatBug.Application.DTOs.Priorities
{
    public class PriorityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public ColorIconDTO ColorIcon { get; set; }
    }
}
