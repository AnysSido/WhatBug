using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Priorities.Queries.GetDeleteConfirm;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Queries.GetDeleteConfirm
{
    [Collection("QueryCollection")]
    public class GetDeleteConfirmQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetDeleteConfirmQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPriorityInfo()
        {
            // Arrange
            var sut = new GetDeleteConfirmQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetDeleteConfirmQuery { PriorityId = 2 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.PriorityId.ShouldBe(2);
            result.Result.Name.ShouldBe("Priority2");
            result.Result.Description.ShouldBe("PriorityDesc2");
        }
    }
}
