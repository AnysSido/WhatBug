using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Priorities.Queries.GetPriorities;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Queries.GetPriorities
{
    [Collection("QueryCollection")]
    public class GetPrioritiesQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPrioritiesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPriorities()
        {
            // Arrange
            var sut = new GetPrioritiesQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPrioritiesQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Priorities.Count.ShouldBe(4);

            var priority = result.Result.Priorities.SingleOrDefault(p => p.Id == 1);
            priority.ShouldNotBeNull();
            priority.Name.ShouldBe("Priority1");
            priority.Description.ShouldBe("PriorityDesc1");
            priority.Order.ShouldBe(4);
            priority.Color.ShouldBe("Color1");
            priority.Icon.ShouldBe("Icon1");
            priority.IconWebName.ShouldBe("icon1");
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPrioritiesInOrder()
        {
            // Arrange
            var sut = new GetPrioritiesQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPrioritiesQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Priorities.Count.ShouldBe(4);
            result.Result.Priorities.Select(p => p.Order).ToArray().ShouldBeEquivalentTo(new int[] { 1, 2, 3, 4 });
        }
    }
}