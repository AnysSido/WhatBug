using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IIssueService
    {
        public Task CreateIssueAsync(CreateIssueDTO dto);
        Task<List<IssueDTO>> GetAllIssuesAsync(int projectId);
    }
}
