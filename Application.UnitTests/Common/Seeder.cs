using System.Linq;
using WhatBug.Domain.Entities;
using WhatBug.Persistence;

namespace WhatBug.Application.UnitTests.Common
{
    public static class Seeder
    {
        public static void SeedUsers(WhatBugDbContext context)
        {
            if (context.Users.Any())
                return;

            context.Users.AddRange(new[]
            {
                new User { Id = 1, Username = "TestUserA", FirstName = "John", Surname = "Smith", Email = "John@smith.com" },
                new User { Id = 2, Username = "TestUserB", FirstName = "Emma", Surname = "Jones", Email = "Emma@jones.com" }
            });

            context.SaveChanges();
        }

        public static void SeedPrioritySchemes(WhatBugDbContext context)
        {
            if (context.PrioritySchemes.Any())
                return;

            context.PrioritySchemes.AddRange(new[]
            {
                new PriorityScheme { Name = "Default" },
                new PriorityScheme { Name = "Software Development" }
            });

            context.SaveChanges();

            return;
        }

        public static void SeedProjects(WhatBugDbContext context)
        {
            if (context.Projects.Any())
                return;

            SeedPrioritySchemes(context);

            context.Projects.AddRange(new[]
            {
                new Project { Id = 1, Key = "PROJ", Name = "Project A", PrioritySchemeId = 1 },
                new Project { Id = 2, Key = "TEST", Name = "Project B", PrioritySchemeId = 2 },
            });

            context.SaveChanges();
        }

        public static void SeedPriorities(WhatBugDbContext context)
        {
            if (context.Priorities.Any())
                return;

            SeedIcons(context);
            SeedColors(context);

            context.Priorities.AddRange(new[]
            {
                new Priority { Id = 1, Name = "Critical", ColorId = 1, IconId = 1 },
                new Priority { Id = 2, Name = "High", ColorId = 2, IconId = 2 },
            });

            context.SaveChanges();
        }

        public static void SeedIssueTypes(WhatBugDbContext context)
        {
            if (context.IssueTypes.Any())
                return;

            SeedIcons(context);
            SeedColors(context);

            context.IssueTypes.AddRange(new[]
            {
                new IssueType { Id = 1, Name = "Task", ColorId = 1, IconId = 1},
                new IssueType { Id = 2, Name = "Bug", ColorId = 2, IconId = 2},
            });

            context.SaveChanges();
        }

        public static void SeedIssueStatuses(WhatBugDbContext context)
        {
            if (context.IssueStatuses.Any())
                return;

            context.IssueStatuses.AddRange(new[]
            {
                new IssueStatus { Id = 1, Name = "Backlog" },
                new IssueStatus { Id = 2, Name = "ToDo" },
            });

            context.SaveChanges();
        }

        public static void SeedColors(WhatBugDbContext context)
        {
            if (context.Colors.Any())
                return;

            context.Colors.AddRange(new[]
            {
                new Color { Id = 1, Name = "Red" },
                new Color { Id = 2, Name = "Blue" },
            });

            context.SaveChanges();
        }

        public static void SeedIcons(WhatBugDbContext context)
        {
            if (context.Icons.Any())
                return;

            context.Icons.AddRange(new[]
            {
                new Icon { Id = 1, Name = "Check" },
                new Icon { Id = 2, Name = "Bug" },
            });

            context.SaveChanges();
        }
    }
}
