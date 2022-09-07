using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using PTP.Core.Repositores;
using Security.Core.Context;
using Security.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Repository.ConcreteRepository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<Users> _userManager;
        private readonly IUnitOfWork<DataContext> _unitOfWork;


        public UserRepo(UserManager<Users> userManager, IUnitOfWork<DataContext> unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> AddUser(Users user, string password)
        {
            try
            {
                return await _userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IdentityResult> AddUserRole(Users userId, string RoleName)
        {
            {
                try
                {
                    return await _userManager.AddToRoleAsync(userId, RoleName);
                }

                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
