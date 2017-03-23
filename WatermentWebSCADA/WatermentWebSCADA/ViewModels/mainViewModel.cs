using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Data.Entity;

namespace WatermentWebSCADA.ViewModels
{
    public class MainViewModel
    {
       //public FacilityViewModel facilitiess { get; set; }
       public int ID { get; set; }

        //facilities
        public string Name { get; set; }
        public string IP { get; set; }
        public string Domain { get; set; }

        //locations model
        public string Address { get; set; }
        public int? Postcode { get; set; }
        public string County { get; set; }
        

        //Countrie model

        public string CountryName { get; set; }
        public string continent { get; set; }

   
        //locations model
 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public float? ProcessValue { get; set; }
        public string Tag { get; set; }
        public  System.DateTime? Timestamp { get; set; }
        public String Description { get; set; }
        public System.DateTime? Alarmsoccured { get; set; }
        public float? AlarmProcessValue { get; set; }
    }

    public class FacilityDBContext : DbContext
    {
        public DbSet<MainViewModel> Facility
        {
            get;
            set;

        }
    }
}