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

    public class FacilityManagement
    {        
        public string StreetAddress { get; set; }
        public Int16 Postcode { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public Int16 locations_id { get; set; }
        public Int16 locations_countries_Id { get; set; }
        public Int16 locations_countries_continents_Id { get; set; }
    }
    /*public class ChartMeasurements : measurements
    {

    }*/

}