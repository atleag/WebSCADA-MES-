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
    /*public class FacilityManagement
    {
        public int ID { get; set; }

        public IEnumerable<facilities> Anlegg { get; set; }
    }*/

    public class AddFacilityViewModel
    {        
        //public string StreetAddress { get; set; }
        //public Int16 Postcode { get; set; }
        //public string County { get; set; }
        //public string City { get; set; }
        //public string Name { get; set; }
        //public Int16 locations_id { get; set; }
        //public Int16 locations_countries_Id { get; set; }
        //public Int16 locations_countries_continents_Id { get; set; }
    }
    /*public class ChartMeasurements : measurements
    {

    }*/
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
        //public int Id { get; set; }
        public string StreetAddress { get; set; }
        public Nullable<int> Postcode { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public int countries_Id { get; set; }
        public int countries_continents_Id { get; set; }
    }

    public class FacilityViewModel
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public string Domain { get; set; }
        public Nullable<int> locations_Id { get; set; }
        public Nullable<int> locations_countries_Id { get; set; }
        public Nullable<int> locations_countries_continents_Id { get; set; }
    }

}