using System.Collections.Generic;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public static class Priorities
    {
        public static IEnumerable<Priority> Seed()
        {
            return new[]
            {
                new Priority { Id = 1, Name = "Medium", Description = "This issue is of average importance.", Order = 4, ColorId = Colors.Shamrock.Id, IconId = Icons.EqualsSign.Id, IsDefault = true },
                new Priority { Id = 2, Name = "Critical", Description = "This task is critical and must be implemented/fixed.", Order = 1, ColorId = Colors.Candy.Id, IconId = Icons.Exclaimation.Id },
                new Priority { Id = 3, Name = "Very High", Description = "This is an important issue that must be resolved.", Order = 2, ColorId = Colors.Rose.Id, IconId = Icons.AnglesUp.Id },
                new Priority { Id = 4, Name = "High", Description = "This issue is quite important.", Order = 3, ColorId = Colors.Orange.Id, IconId = Icons.AngleUp.Id },
                new Priority { Id = 5, Name = "Low", Description = "Low priority issue.", Order = 5, ColorId = Colors.Cerulean.Id, IconId = Icons.AngleDown.Id },
                new Priority { Id = 6, Name = "Very Low", Description = "Other issues should take priority.", Order = 6, ColorId = Colors.Cobalt.Id, IconId = Icons.AnglesDown.Id },
                new Priority { Id = 7, Name = "Trivial", Description = "May or may not be dealt with depending on time.", Order = 7, ColorId = Colors.Violet.Id, IconId = Icons.CircleArrowDown.Id }
            };
        }
    }
}
