using System;
using WhatBug.Persistence;
using Xunit;

namespace WhatBug.Application.UnitTests.Common
{
    public class ValidatorTestFixture : IDisposable
    {
        public WhatBugDbContext Context;

        public ValidatorTestFixture()
        {
            var guid = Guid.NewGuid().ToString();
            var factory = new WhatBugContextFactory();

            factory.CreateWithSeed(guid);
            Context = factory.Create(guid);
        }

        public void Dispose()
        {
            WhatBugContextFactory.Dispose(Context);
        }
    }

    [CollectionDefinition("ValidatorCollection")]
    public class ValidatorCollection : ICollectionFixture<ValidatorTestFixture> { }
}