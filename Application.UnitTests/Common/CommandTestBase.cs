using System;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.UnitTests.Common;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly IWhatBugDbContext _context;
        private readonly WhatBugContextFactory _factory;
        private string _dbName = Guid.NewGuid().ToString();

        public CommandTestBase()
        {
            _factory = new WhatBugContextFactory();
            _context = _factory.Create(_dbName);
        }

        public IWhatBugDbContext CreateContext()
        {
            return _factory.Create(_dbName);
        }

        public void Dispose()
        {
            WhatBugContextFactory.Dispose(_context);
        }
    }
}