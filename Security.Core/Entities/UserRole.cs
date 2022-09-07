using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Core.Entities
{
    public class UserRole: IdentityUserRole<int>
    {
        public Users User { get; set; }
        public Role Role { get; set; }
    }
}
