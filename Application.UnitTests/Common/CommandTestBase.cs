using System;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Persistence;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly WhatBugDbContext _context;
        protected readonly WhatBugContextFactory _factory;

        public CommandTestBase()
        {
            _factory = new WhatBugContextFactory();
            _context = _factory.Create();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _factory.Dispose();
        }
    }
}
