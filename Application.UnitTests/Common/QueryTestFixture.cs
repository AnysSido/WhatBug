using AutoMapper;
using System;
using WhatBug.Application.Common;
using WhatBug.Persistence;
using Xunit;

namespace WhatBug.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public WhatBugDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            var guid = Guid.NewGuid().ToString();
            var factory = new WhatBugContextFactory();

            factory.CreateWithSeed(guid);
            Context = factory.Create(guid);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            WhatBugContextFactory.Dispose(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}