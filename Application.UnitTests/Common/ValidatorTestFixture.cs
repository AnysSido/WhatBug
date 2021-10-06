using System;
using WhatBug.Persistence;
using Xunit;

namespace WhatBug.Application.UnitTests.Common
{
    public class ValidatorTestFixture
    {
        private readonly WhatBugContextFactory _factory;
        private readonly string _guid;

        public ValidatorTestFixture()
        {
            _guid = Guid.NewGuid().ToString();
            _factory = new WhatBugContextFactory();

            _factory.CreateWithSeed(_guid);
        }

        public WhatBugDbContext CreateContext()
        {
            return _factory.Create(_guid);
        }
    }

    [CollectionDefinition("ValidatorCollection")]
    public class ValidatorCollection : ICollectionFixture<ValidatorTestFixture> { }
}