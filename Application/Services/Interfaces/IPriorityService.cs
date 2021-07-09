using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPriorityService
    {
        Task CreatePriorityAsync(PriorityDTO dto);
        Task<List<PriorityDTO>> GetPrioritiesAsync();
        Task<PriorityDTO> GetPriorityAsync(int id);
        Task<List<PriorityIconDTO>> LoadIconsAsync();
        Task UpdatePriority(PriorityDTO dto);
        Task UpdatePriorityOrder(List<int> ids);
    }
}
