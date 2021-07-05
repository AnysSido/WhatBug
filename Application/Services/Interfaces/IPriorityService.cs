using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Priorities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPriorityService
    {
        Task CreatePriorityAsync(PriorityDTO dto);
    }
}
