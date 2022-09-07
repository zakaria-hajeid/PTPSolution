using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Core.Entities
{
    public class Users : IdentityUser<int>
    {
       
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
