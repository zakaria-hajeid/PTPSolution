using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Security.Core.Entities
{
    public class Users : IdentityUser<int>
    {
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
