using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatermentWebSCADA.ViewModels
{
    public class UserManagement
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Username { get; set; }
        public string location_Address { get; set; }
        public int location_Postcode { get; set; }
        public string location_country_CountryName { get; set; }
      
    }
  
}