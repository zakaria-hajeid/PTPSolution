using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Infrastructure.Proxies.Request
{
    [Serializable]
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string name { get; set; }
    }
}
