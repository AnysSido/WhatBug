using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Priorities.Queries.GetEditPriority;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Queries.GetEditPriority
{
    [Collection("QueryCollection")]
    public class GetEditPriorityQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPriorityQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPriorityWithIconsAndColors()
        {
            // Arrange
            var sut = new GetEditPriorityQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEditPriorityQuery { Id = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Id.ShouldBe(1);
            result.Result.Name.ShouldBe("Priority1");
            result.Result.Description.ShouldBe("PriorityDesc1");
            result.Result.IconId.ShouldBe(1);
            result.Result.ColorId.ShouldBe(1);
            result.Result.Order.ShouldBe(4);
            result.Result.Colors.Count.ShouldBe(3);
            result.Result.Icons.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsColorsInOrder()
        {
            // Arrange
            var sut = new GetEditPriorityQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEditPriorityQuery { Id = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Colors.Select(c => c.Id).ToArray().ShouldBeEquivalentTo(new[] { 1, 2, 3});
        }
    }
}