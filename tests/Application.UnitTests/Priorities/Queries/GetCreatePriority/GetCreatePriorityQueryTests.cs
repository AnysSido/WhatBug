using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Priorities.Queries.GetCreatePriority;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Queries.GetCreatePriority
{
    [Collection("QueryCollection")]
    public class GetCreatePriorityQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreatePriorityQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsAllIcons()
        {
            // Arrange
            var sut = new GetCreatePriorityQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetCreatePriorityQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Icons.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsAllColorsInOrder()
        {
            // Arrange
            var sut = new GetCreatePriorityQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetCreatePriorityQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Colors.Count.ShouldBe(3);
            result.Result.Colors.Select(c => c.Id).ToArray().ShouldBeEquivalentTo(new[] { 1, 2, 3 });
        }
    }
}