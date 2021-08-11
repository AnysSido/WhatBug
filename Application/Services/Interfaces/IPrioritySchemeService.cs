using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.PrioritySchemes;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPrioritySchemeService
    {
        Task CreatePrioritySchemeAsync(CreatePrioritySchemeDTO prioritySchemeDTO);
        Task<PrioritySchemeDTO> GetPrioritySchemeAsync(int id);
        Task EditPrioritySchemeAsync(EditPrioritySchemeDTO dto);
    }
}
