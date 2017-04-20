using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WatermentWebSCADA.ViewModels
{

    /*public class AddFacilityViewModel
    {

    }

    public class ContinentsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class CountriesViewModel
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public int continents_Id { get; set; }
    }*/
    public class MaintenanceViewModel
    {
        public int OrderId { get; set; }
        public string Person { get; set; }
        public string Description { get; set; }

        [Display(Name = "Date of last Maintenance")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> LastMaintenance { get; set; }
        public int facilities_Id { get; set; }

        public virtual facilities facilities { get; set; }
    }

    public class FacilityViewModel
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string Domain { get; set; }
        public Nullable<int> locations_Id { get; set; }
        public Nullable<int> locations_countries_Id { get; set; }
        public Nullable<int> locations_countries_continents_Id { get; set; }

    }


}