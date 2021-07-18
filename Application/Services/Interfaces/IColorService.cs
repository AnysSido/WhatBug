using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Common;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IColorService
    {
        public Task<List<ColorDTO>> GetAllAsync();
    }
}
