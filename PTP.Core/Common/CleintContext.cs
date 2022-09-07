using PTP.Core.Common.Secuerty;
using System;
using System.Collections.Generic;
using PTP.Core.Common.Extensions;

using Microsoft.AspNetCore.Http;

namespace PTP.Core.Common
{
    public class CleintContext : ICleintContext
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        public CleintContext(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

        }
        public string GetAuthHash()
        {
            throw new NotImplementedException();
        }
        
        public string GetFirstNameAr()
        {
            throw new NotImplementedException();
        }

        public string GetFirstNameEn()
        {
            throw new NotImplementedException();
        }

        public string GetLanguage()
        {
            throw new NotImplementedException();
        }

        public string GetLastNameAr()
        {
            throw new NotImplementedException();
        }

        public string GetLastNameEn()
        {
            throw new NotImplementedException();
        }

        public string GetPasswordHash()
        {
            throw new NotImplementedException();
        }

        public int? GetUserId()
        {
            string currentUserId = GetCalimValue(SecurityJwtClaimTypes.UserId);

            if (int.TryParse(currentUserId, out int userId))
            {
                return userId;
            }
            else
            {
                return null;
            }
        }

        public string GetUsername()
        {
            throw new NotImplementedException();
        }

        public string Token()
        {
            return GetCalimValue(SecurityJwtClaimTypes.Token);
        }


        private string GetCalimValue(string cliam)
        {
            return HttpContextAccessor.HttpContext.User?.Claims?.GetValue(cliam);
        }
    }
}
