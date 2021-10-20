using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Commands.EditPriority;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands.EditPriorities
{
    public class EditPriorityCommandTests : CommandTestBase
    {
        public EditPriorityCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Priorities.AddRange(new[]
                {
                    new Priority { Id = 1, Name = "Name1", Description = "Desc1", Order = 1, ColorId = 1, IconId = 1 },
                    new Priority { Id = 2, Name = "Name2", Description = "Desc2", Order = 2, ColorId = 2, IconId = 1 },
                });
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
        public async Task Handle_GivenValidRequest_UpdatesPriority()
        {
            // Arrange
            var sut = new EditPriorityCommandHandler(_context);
            var command = new EditPriorityCommand { Id = 1, Name = "NewName", Description = "NewDesc", IconId = 1, ColorId = 1 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var priority = _context.Priorities.SingleOrDefault(p => p.Id == command.Id);

            // Assert
            result.Succeeded.ShouldBe(true);
            priority.ShouldNotBeNull();
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
            var sut = new EditPriorityCommandHandler(_context);
            var command = new EditPriorityCommand { Id = 1, Name = "NewName", Description = description, IconId = 1, ColorId = 1 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var priority = _context.Priorities.SingleOrDefault(p => p.Id == command.Id);

            // Assert
            result.Succeeded.ShouldBe(true);
            priority.Description.ShouldBe(null);
        }
    }
}