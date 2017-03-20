using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}