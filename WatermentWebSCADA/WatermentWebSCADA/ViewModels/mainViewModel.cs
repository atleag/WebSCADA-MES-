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
    public class MainViewModel
    {
      
        
        public int ID { get; set; }
        public IEnumerable<facilities> Facilites { get; set; }
        public IEnumerable<alarms> Alarms { get; set; }
        public IEnumerable<countries> Countries { get; set; }
 
        public IEnumerable<equipments> Equipments { get; set; }
        public IEnumerable<locations> Locations { get; set; }
        public IEnumerable<maintenance> Maintenance { get; set; }
        public int equipments_Id { get; set; }
        public string Tag { get; set; }
        public virtual equipments equipments { get; set; }
     
        public IEnumerable<User> Users { get; set; }
        public List<countries_with_facilites_view>  countries { get; set; }  
        public List<equipments> Equipment { get; set; }
        

        public int NumberOfFacilities { get; internal set; }
        public int NumberOnline { get; internal set; }
        public int NumberOffline { get; internal set; }
        public int noAlarms { get; internal set; }
    }

    public class FacilitesAlarmModel
    {
        public string Name { get; set; }
    }
}

