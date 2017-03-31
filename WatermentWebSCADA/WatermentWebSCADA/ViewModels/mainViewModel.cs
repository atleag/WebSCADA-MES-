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
    

      

    public IEnumerable<facilities> Anlegg { get; set; }
    public IEnumerable<alarms> Alarmer { get; set; }
        public IEnumerable<continents> Kontinenter { get; set; }
        public IEnumerable<countries> Land { get; set; }
        public IEnumerable<equipments> Utstyr { get; set; }
        public IEnumerable<locations> Lokasjoner { get; set; }
        public IEnumerable<maintenance> Vedlikehold { get; set; }
        public IEnumerable<roles> Roller { get; set; }
        public IEnumerable<sessions> Sesjoner { get; set; }
        public IEnumerable<users> Brukere { get; set; }
        public List<measurements> Verdier { get; set; }


    }

}

