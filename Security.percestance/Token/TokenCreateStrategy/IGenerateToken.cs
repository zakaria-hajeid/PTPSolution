using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Security.percestance.Token.TokenCreateStrategy
{
    public interface IGenerateToken
    {
        Task<object> CreateToken(params object[] Credentials);
    }
}
