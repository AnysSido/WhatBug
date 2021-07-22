using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WhatBug.WebUI.Authorization.RequirePermissionAttribute;

namespace WhatBug.WebUI.Authorization
{
    /*
        Custom policy provider to build dynamic policies and requirements from RequirePermissionAttributes.
        The RequirePermissionAttribute builds a unique policy name representing the required
        permissions and the operator (and/or).

        This provider extracts the operator and required permissions from the policy name and instantiates 
        a new PermissionRequirement with these paramaters. 
        This requirement is then added to a new policy and returned to the framework.
     */
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            // If this is not a dynamic permission policy, return to the default behaviour
            // of loading policies registered in startup.cs.
            if (!policyName.StartsWith(PolicyPrefix, StringComparison.OrdinalIgnoreCase))
                return await base.GetPolicyAsync(policyName);

            // Extract operator and permissions from the policy name and construct a new requirement.
            var requirement = new PermissionRequirement(GetOperatorFromPolicy(policyName), GetPermissionsFromPolicy(policyName));

            // Build a new policy containing the new requirement build from the policy name.
            return new AuthorizationPolicyBuilder().AddRequirements(requirement).Build();
        }
    }
}