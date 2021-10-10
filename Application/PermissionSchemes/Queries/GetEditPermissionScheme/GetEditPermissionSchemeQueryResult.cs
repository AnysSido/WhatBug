using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Queries.GetEditPermissionScheme
{
    public class GetEditPermissionSchemeQueryResult : IMapFrom<PermissionScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}