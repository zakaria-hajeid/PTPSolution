using System;
using System.Collections;
using System.Collections.Generic;

namespace Security.Core.Dtos
{
    public class LoginDtos
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginResult
    {
        public string Token { get; set; }
    }

}
