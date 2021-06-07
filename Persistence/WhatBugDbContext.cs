using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Persistence
{
    public class WhatBugDbContext : IdentityDbContext, IWhatBugDbContext
    {
        public WhatBugDbContext(DbContextOptions<WhatBugDbContext> options)
            : base(options)
        {
        }
    }
}
