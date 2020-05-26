using Microsoft.AspNetCore.Authorization;
using System;

namespace MvcAuthorize.PermissionAccess
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(params Permissions[] permission) : base(string.Join(";", permission))
        { }
    }
}
