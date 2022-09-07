using Microsoft.AspNetCore.Identity;
using Security.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Repository
{
    public interface IUserRepo
    {
        Task<IdentityResult> AddUser(Users user,string password);
        Task<IdentityResult> AddUserRole(Users userId, string RoleName);

    }
}
