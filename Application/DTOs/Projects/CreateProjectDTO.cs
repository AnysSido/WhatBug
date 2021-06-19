using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.DTOs.Projects
{
    public class CreateProjectDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
