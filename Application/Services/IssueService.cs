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

            // TODO: Remove magic string
            var issueStatus = await _context.IssueStatuses.Where(s => s.Name == "Backlog").FirstAsync();
            var issue = _mapper.Map<Issue>(dto);
            issue.IssueStatus = issueStatus;
            await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();
        }

        public async Task<List<IssueTypeDTO>> GetIssueTypesAsync()
        {
            // TODO: Check permissions
            return _mapper.Map<List<IssueTypeDTO>>(
                await _context.IssueTypes
                    .Include(i => i.ColorIcon.Color)
                    .Include(i => i.ColorIcon.Icon)
                    .ToListAsync());
        }

        public async Task<List<IssueStatusDTO>> GetIssueStatusesAsync()
        {
            // TODO: Check permissions
            return _mapper.Map<List<IssueStatusDTO>>(await _context.IssueStatuses.ToListAsync());
        }
    }
}
