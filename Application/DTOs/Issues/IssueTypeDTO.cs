using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Common;

namespace WhatBug.Application.DTOs.Issues
{
    public class IssueTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColorIconDTO ColorIcon { get; set; }
    }
}
