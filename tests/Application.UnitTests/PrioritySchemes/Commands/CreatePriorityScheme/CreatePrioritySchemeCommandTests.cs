using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Commands.CreatePriorityScheme
{
    public class CreatePrioritySchemeCommandTests : CommandTestBase
    {
        public CreatePrioritySchemeCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Priorities.AddRange(new[]
                {
                    new Priority { Id = 1, Name = "Prio1" },
                    new Priority { Id = 2, Name = "Prio2" },
                    new Priority { Id = 3, Name = "Prio3", IsDefault = true },
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_CreatesPriorityScheme()
        {
            // Arrange
            var sut = new CreatePrioritySchemeCommandHandler(_context);
            var command = new CreatePrioritySchemeCommand
            {
                Name = "TestName",
                Description = "TestDesc",
                PriorityIds = new[] { 1, 3 }
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var scheme = _context.PrioritySchemes.First();

            // Assert
            result.Succeeded.ShouldBe(true);
            scheme.ShouldNotBeNull();
            scheme.Name.ShouldBe("TestName");
            scheme.Description.ShouldBe("TestDesc");
            scheme.Priorities.Count.ShouldBe(2);
            scheme.Priorities.Select(p => p.PriorityId).ShouldContain(1);
            scheme.Priorities.Select(p => p.PriorityId).ShouldContain(3);
        }

        [Fact]
        public async Task Handle_GivenNoPriorities_AssignsDefaultPriority()
        {
            // Arrange
            var sut = new CreatePrioritySchemeCommandHandler(_context);
            var command = new CreatePrioritySchemeCommand
            {
                Name = "TestName",
                Description = "TestDesc",
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var scheme = _context.PrioritySchemes.First();

            // Assert
            result.Succeeded.ShouldBe(true);
            scheme.ShouldNotBeNull();
            scheme.Priorities.Count.ShouldBe(1);
            scheme.Priorities.First().PriorityId.ShouldBe(3);
        }
    }
}