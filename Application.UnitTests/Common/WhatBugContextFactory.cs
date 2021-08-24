using Microsoft.EntityFrameworkCore;
using Moq;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Persistence;

namespace WhatBug.Application.UnitTests.Common
{
    public class WhatBugContextFactory
    {
        private DbContextOptions<WhatBugDbContext> _options;
        private ICurrentUserService _currentUserService;

        public WhatBugDbContext Create(string guid)
        {
            if (_options == null)
            {
                _options = new DbContextOptionsBuilder<WhatBugDbContext>()
                    .UseInMemoryDatabase(guid).Options;

                var mockUserService = new Mock<ICurrentUserService>();
                mockUserService.SetupGet(x => x.Id).Returns(1);
                _currentUserService = mockUserService.Object;
            }

            return new WhatBugDbContext(_options, _currentUserService);
        }

        public void Dispose(WhatBugDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
