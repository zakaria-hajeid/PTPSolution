using Security.Core.Dtos;
using Security.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Security.percestance
{
    public interface ITokenService
    {
        Task<LoginResult> CreateJwtTokenn(Users loginInformation, IList<string> Roles);

    }
}
