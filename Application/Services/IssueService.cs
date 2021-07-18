using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services
{
    class IssueService : IIssueService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public IssueService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateIssueAsync(CreateIssueDTO dto)
        {
            // TODO: Check permissions
            var issue = _mapper.Map<Issue>(dto);
            await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();
        }

        public async Task<List<IssueDTO>> GetAllIssuesAsync(int projectId)
        {
            // TODO: Check permissions
            return _mapper.Map<List<IssueDTO>>(await _context.Issues
                .Include(i => i.Assignee)
                .Include(i => i.Reporter)
                .Include(i => i.Priority)
                    .ThenInclude(p => p.ColorIcon)
                        .ThenInclude(ci => ci.Color)
                .Include(i => i.Priority)
                    .ThenInclude(p => p.ColorIcon)
                        .ThenInclude(ci => ci.Icon)
                .Where(i => i.ProjectId == projectId).ToListAsync());
        }

        public async Task<IssueDTO> GetIssue(int issueId)
        {
            // TODO: Check permissions
            return _mapper.Map<IssueDTO>(
                await _context.Issues
                    .Include(i => i.Assignee)
                    .Include(i => i.Reporter)
                    .Include(i => i.Priority)
                        .ThenInclude(p => p.ColorIcon)
                            .ThenInclude(ci => ci.Color)
                    .Include(i => i.Priority)
                        .ThenInclude(p => p.ColorIcon)
                            .ThenInclude(ci => ci.Icon)
                    .FirstOrDefaultAsync(i => i.Id == issueId));
        }
    }
}
