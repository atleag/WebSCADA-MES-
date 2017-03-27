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
        public string Name { get; set; }

      

    public IEnumerable<facilities> Anlegg { get; set; }
        public IEnumerable<alarms> Alarmer { get; set; }
    }

}

