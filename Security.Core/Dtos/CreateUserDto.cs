﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Core.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
