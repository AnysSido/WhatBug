using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatBug.Persistence
{
    public class WhatBugDbContext : IdentityDbContext
    {
        public WhatBugDbContext(DbContextOptions<WhatBugDbContext> options)
            : base(options)
        {
        }
    }
}
