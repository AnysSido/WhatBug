using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Queries.GetEditPriorityScheme
{
    [Collection("QueryCollection")]
    public class GetEditPrioritySchemeQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPrioritySchemeQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPriorityScheme()
        {
            // Arrange
            var sut = new GetEditPrioritySchemeQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEditPrioritySchemeQuery { Id = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Id.ShouldBe(1);
            result.Result.Name.ShouldBe("PriorityScheme1");
            result.Result.Description.ShouldBe("PrioritySchemeDesc1");
            result.Result.PriorityIds.Count.ShouldBe(3);
            result.Result.PriorityIds.ToArray().ShouldBeEquivalentTo(new[] { 1, 2, 4 });
            result.Result.Priorities.Count.ShouldBe(4);
            result.Result.Priorities.Select(p => p.Id).OrderBy(p => p).ToArray().ShouldBeEquivalentTo(new[] { 1, 2, 3, 4 });
        }
    }
}