using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTP.Core.Common.Attributes;
using PTP.Core.Common.Utilities;

namespace PTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ControllerBase : Controller
    {
        protected int GetActiveUserId()
        {
            return IdentityManager.UserId.Value;
        }
        protected string GetToken()
        {
            return IdentityManager.Token;
        }

        protected void CheckPermissions(string permissions)
        {

            //NEW BUSSNISS 
            /* if (!hasPermission)
         {
               throw new UnauthorizedAccessException();
           }*/
        }
    }
}
