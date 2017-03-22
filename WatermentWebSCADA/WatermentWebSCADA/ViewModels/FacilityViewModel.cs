using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
namespace WatermentWebSCADA.ViewModels
{
    public class FacilityViewModel
    {
        public List<string> Name { get; set; }
        public List<string> IP { get; set; }
        public List<string> Domain { get; set; }
        //locations model
        public List<string> Address { get; set; }
        public List<int> Postcode { get; set; }
        public List<string> County { get; set; }
        public List<string> CountryName { get; set; }
        public List<string> FirstName { get; set; }
        public List<string> LastName { get; set; }
        public List<int?> Phone { get; set; }
        public List<string> Email { get; set; }
        public List<float?> ProcessValue { get; set; }
        public List<string> Tag { get; set; }
        public List<System.DateTime> Timestamp { get; set; }
        public List<String> Description { get; set; }
        public List<System.DateTime> Alarmsoccured { get; set; }
    }
}