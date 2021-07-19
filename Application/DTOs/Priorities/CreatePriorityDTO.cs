using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Common;

namespace WhatBug.Application.DTOs.Priorities
{
    public class CreatePriorityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public int IconId { get; set; }
    }
}
