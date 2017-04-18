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
    }
    public class LocationViewModel
    {
        public string StreetAddress { get; set; }
        public Nullable<int> Postcode { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public int countries_Id { get; set; }
        public int countries_continents_Id { get; set; }
    }*/

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
    public class FacilityEquipmentVM
    {
        public string Tag { get; set; } //Equipment
        public string SIUnits { get; set; }//Equipment
        public string Description { get; set; }//Equipment
        public Nullable<System.DateTime> LastCalibrated { get; set; } //Equipment
        
    }
    public class FacilityAddEquipmentVM
    {
        [Required]
        [Display(Name = "Tag, such as TT101")]
        public string Tag { get; set; } //Equipment
        [Required]
        [Display(Name = "SI unit")]
        public string SIUnits { get; set; }//Equipment
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }//Equipment
        [Required]
        public int facilities_Id { get; set; }

    }

}