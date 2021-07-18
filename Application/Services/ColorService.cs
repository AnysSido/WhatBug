using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.Application.Services
{
    class ColorService : IColorService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public ColorService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ColorDTO>> GetAllAsync()
        {
            return _mapper.Map<List<ColorDTO>>(await _context.Colors.ToListAsync());
        }
    }
}
