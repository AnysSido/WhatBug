using System;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Persistence;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly WhatBugDbContext _context;
        private readonly WhatBugContextFactory _factory;
        private string _dbName = Guid.NewGuid().ToString();

        public CommandTestBase()
        {
            _factory = new WhatBugContextFactory();
            _context = _factory.Create(_dbName);
        }

        public WhatBugDbContext CreateContext()
        {
            return _factory.Create(_dbName);
        }

        public void Dispose()
        {
            _factory.Dispose(_context);
        }
    }
}
