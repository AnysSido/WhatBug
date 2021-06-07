using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.Application.Services
{
    class IssueService : IIssueService
    {
        private readonly IWhatBugDbContext _context;

        public IssueService(IWhatBugDbContext context)
        {
            _context = context;
        }

        public void CreateIssue(string name, string description)
        {
            throw new NotImplementedException();
        }
    }
}
