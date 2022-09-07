using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using System.Linq;

namespace PTP.Core.Common.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetValue(this IEnumerable<Claim> claims, string claimType)
        {
            string value = null;

            Claim userClaim = claims.FirstOrDefault(x => x.Type == claimType);
            if (userClaim != null)
            {
                value = userClaim.Value;
            }

            return value;
        }
    }

}
