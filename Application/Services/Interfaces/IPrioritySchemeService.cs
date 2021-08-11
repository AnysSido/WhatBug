using System.Threading.Tasks;
using WhatBug.Application.DTOs.PrioritySchemes;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPrioritySchemeService
    {
        Task<PrioritySchemeDTO> GetPrioritySchemeAsync(int id);
        Task EditPrioritySchemeAsync(EditPrioritySchemeDTO dto);
    }
}
