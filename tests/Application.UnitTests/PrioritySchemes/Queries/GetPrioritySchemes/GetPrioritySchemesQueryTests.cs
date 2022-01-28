using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Queries.GetPrioritySchemes
{
    [Collection("QueryCollection")]
    public class GetPrioritySchemesQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPrioritySchemesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPrioritySchemes()
        {
            // Arrange
            var sut = new GetPrioritySchemesQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPrioritySchemesQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.PrioritySchemes.Count.ShouldBe(3);

            var scheme = result.Result.PrioritySchemes.SingleOrDefault(s => s.Id == 1);
            scheme.ShouldNotBeNull();
            scheme.IsDefault.ShouldBe(false);
            scheme.Name.ShouldBe("PriorityScheme1");
            scheme.Description.ShouldBe("PrioritySchemeDesc1");
            scheme.Priorities.Count.ShouldBe(3);
            scheme.Priorities.Select(p => p.Name).ToArray().ShouldBeEquivalentTo(new[] { "Priority4", "Priority2", "Priority1" });

            scheme = result.Result.PrioritySchemes.SingleOrDefault(s => s.Id == 2);
            scheme.ShouldNotBeNull();
            scheme.IsDefault.ShouldBe(false);
            scheme.Name.ShouldBe("PriorityScheme2");
            scheme.Description.ShouldBe("PrioritySchemeDesc2");
            scheme.Priorities.Count.ShouldBe(2);
            scheme.Priorities.Select(p => p.Name).ToArray().ShouldBeEquivalentTo(new[] { "Priority2", "Priority1" });

            scheme = result.Result.PrioritySchemes.SingleOrDefault(s => s.Id == 3);
            scheme.ShouldNotBeNull();
            scheme.IsDefault.ShouldBe(true);
            scheme.Name.ShouldBe("PriorityScheme3");
            scheme.Description.ShouldBe("PrioritySchemeDesc3");
            scheme.Priorities.Count.ShouldBe(0); // TODO: This should contain all priorities
        }
    }
}