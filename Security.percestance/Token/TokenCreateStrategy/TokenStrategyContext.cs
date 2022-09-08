using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Security.percestance.Token.TokenCreateStrategy
{
    public class TokenStrategyContext
    {
        private IGenerateToken generateToken;


        public TokenStrategyContext(IGenerateToken generateToken)
        {
            this.generateToken = generateToken;
        }

        public Task<object> CreateToken (params object[] credentails)
        {
           return generateToken.CreateToken(credentails);
        }
    }
}
