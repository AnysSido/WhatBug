using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Commands.CreatePriority;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands.CreatePriority
{
    public class CreatePriorityCommandTests : CommandTestBase
    {
        public CreatePriorityCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Colors.AddRange(new[]
                {
                    new Color { Id = 1, Name = "Color1" },
                    new Color { Id = 2, Name = "Color2" },
                });

                context.Icons.Add(new Icon { Id = 1, Name = "Icon1", WebName = "icon1" });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_CreatesPriority()
        {
            // Arrange
            var sut = new CreatePriorityCommandHandler(_context);
            var command = new CreatePriorityCommand { Name = "NewName", Description = "NewDesc", IconId = 1, ColorId = 1 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var priority = _context.Priorities.FirstOrDefault();

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.ShouldBe(priority.Id);
            priority.ShouldNotBeNull();
            priority.Id.ShouldNotBe(default(int));
            priority.Name.ShouldBe("NewName");
            priority.Description.ShouldBe("NewDesc");
            priority.IconId.ShouldBe(1);
            priority.ColorId.ShouldBe(1);
            priority.Order.ShouldBe(1);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Handle_GivenNullOrEmptyDescription_SetsNullDescription(string description)
        {
            // Arrange
            var sut = new CreatePriorityCommandHandler(_context);
            var command = new CreatePriorityCommand { Name = "NewName", Description = description, IconId = 1, ColorId = 1 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var priority = _context.Priorities.FirstOrDefault();

            // Assert
            result.Succeeded.ShouldBe(true);
            priority.Description.ShouldBe(null);
        }
    }
}