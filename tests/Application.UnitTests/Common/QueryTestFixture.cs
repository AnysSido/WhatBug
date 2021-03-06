using AutoMapper;
using System;
using WhatBug.Application.Common;
using WhatBug.Application.Common.Interfaces;
using Xunit;

namespace WhatBug.Application.UnitTests.Common
{
    public class QueryTestFixture
    {
        public IMapper Mapper;
        private string _guid;
        private WhatBugContextFactory _factory;

        public QueryTestFixture()
        {
            _guid = Guid.NewGuid().ToString();
            _factory = new WhatBugContextFactory();
            _factory.CreateWithSeed(_guid);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public IWhatBugDbContext CreateContext()
        {
            return _factory.Create(_guid.ToString());
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}