using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetPermissionSchemes
{
    [Collection("QueryCollection")]
    public class GetPermissionSchemesQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPermissionSchemesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPermissionSchemes()
        {
            // Arrange
            var sut = new GetPermissionSchemesQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPermissionSchemesQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.PermissionSchemes.Count.ShouldBe(3);

            var scheme = result.Result.PermissionSchemes.SingleOrDefault(p => p.Id == 1);
            scheme.ShouldNotBeNull();
            scheme.Name.ShouldBe("PermissionScheme1");
            scheme.Description.ShouldBe("PermissionSchemeDesc1");

            scheme = result.Result.PermissionSchemes.SingleOrDefault(p => p.Id == 2);
            scheme.ShouldNotBeNull();
            scheme.Name.ShouldBe("PermissionScheme2");
            scheme.Description.ShouldBe("PermissionSchemeDesc2");

            scheme = result.Result.PermissionSchemes.SingleOrDefault(p => p.Id == 3);
            scheme.ShouldNotBeNull();
            scheme.Name.ShouldBe("PermissionScheme3");
            scheme.Description.ShouldBe("PermissionSchemeDesc3");
        }
    }
}
