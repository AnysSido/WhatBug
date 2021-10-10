using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes
{
    public class GetPermissionSchemesQueryResult
    {
        public IList<PermissionSchemeDTO> PermissionSchemes { get; set; }
    }

    public class PermissionSchemeDTO : IMapFrom<PermissionScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
