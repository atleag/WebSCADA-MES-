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
        public IEnumerable<facilities> Facilites { get; set; }
        public IEnumerable<alarms> Alarmer { get; set; }
        public IEnumerable<countries> Countries { get; set; }
 
        public IEnumerable<equipments> Utstyr { get; set; }
        public IEnumerable<locations> Lokasjoner { get; set; }
        public IEnumerable<maintenance> Vedlikehold { get; set; }
        public IEnumerable<User> Brukere { get; set; }
        public List<countries_with_facilites_view>  countries { get; set; }  
        public List<equipments> Equipment { get; set; }

       
        public int antallFacilities { get; internal set; }
        public int antallOnline { get; internal set; }
        public int antallOffline { get; internal set; }
        public int noAlarms { get; internal set; }
    }

}

