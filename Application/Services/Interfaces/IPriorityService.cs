using System.Collections.Generic;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.DTOs.Priorities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPriorityService
    {
        Task<List<PriorityDTO>> GetPrioritiesAsync();
        Task<List<IconDTO>> LoadIconsAsync();
        Task UpdatePriorityOrderAsync(List<int> ids);
    }
}
