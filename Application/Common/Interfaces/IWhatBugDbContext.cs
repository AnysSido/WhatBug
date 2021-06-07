using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IWhatBugDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
