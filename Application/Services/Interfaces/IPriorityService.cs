using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPriorityService
    {
        Task<List<PriorityDTO>> GetPrioritiesAsync();
        Task<PriorityDTO> GetPriorityAsync(int id);
        Task<List<IconDTO>> LoadIconsAsync();
        Task EditPriorityAsync(EditPriorityDTO dto);
        Task UpdatePriorityOrderAsync(List<int> ids);
    }
}
