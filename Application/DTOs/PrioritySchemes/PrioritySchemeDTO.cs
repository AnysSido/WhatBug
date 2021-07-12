using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Priorities;

namespace WhatBug.Application.DTOs.PrioritySchemes
{
    public class PrioritySchemeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PriorityDTO> Priorities { get; set; }
    }
}
