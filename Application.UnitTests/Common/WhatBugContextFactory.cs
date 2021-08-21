using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Data.Common;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Persistence;

namespace WhatBug.Application.UnitTests.Common
{
    public class WhatBugContextFactory : IDisposable
    {
        private DbConnection _connection;
        private DbContextOptions<WhatBugDbContext> _options;
        private ICurrentUserService _currentUserService;

        public WhatBugDbContext Create()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                _options = new DbContextOptionsBuilder<WhatBugDbContext>()
                .UseSqlite(_connection).Options;

                var mockUserService = new Mock<ICurrentUserService>();
                mockUserService.SetupGet(x => x.Id).Returns(1);
                _currentUserService = mockUserService.Object;

                using (var context = new WhatBugDbContext(_options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new WhatBugDbContext(_options, _currentUserService);
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
