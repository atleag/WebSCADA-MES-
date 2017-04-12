using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Data.Entity;

namespace WatermentWebSCADA.ViewModels
{
    public class FacilityManagement
    {
        public int ID { get; set; }

        public IEnumerable<facilities> Anlegg { get; set; }
    }

    public class AddFacility
    {
        public string StreetAddress { get; set; }
        public int Postcode { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string locations_id { get; set; }
        public string locations_countries_Id { get; set; }
        public string locations_countries_continents_Id { get; set; }

    }
    public class ChartMeasurements : measurements
    {

    }

}