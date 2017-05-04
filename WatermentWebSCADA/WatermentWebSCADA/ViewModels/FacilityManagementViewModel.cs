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

    public class MaintenanceViewModel
    {
        public int OrderId { get; set; }
        public string Person { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(250,ErrorMessage ="Max 250 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A date must be selected")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of last Maintenance")]
        public System.DateTime LastMaintenance { get; set; }
        public int facilities_Id { get; set; }
        public IEnumerable<equipments> Equipments { get; set; }
        public IEnumerable<locations> Location { get; set; }
        public IEnumerable<maintenance> Maintenance { get; set; }
        public IEnumerable<facilities> Facilites { get; set; }
        public int ID { get; set; }

        public int equipments_Id { get; set; }
        public string Tag { get; set; }
        public virtual equipments equipments { get; set; }


        public List<equipments> Equipment { get; set; }
    }

    public class FacilityViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string Domain { get; set; }
        public string SerialNumber { get; set; }
        public string ProgramVersion { get; set; }
        public Nullable<int> locations_Id { get; set; }
        public Nullable<int> locations_countries_Id { get; set; }
        public Nullable<int> locations_countries_continents_Id { get; set; }
        public Nullable<int> FacilityStatus_Id { get; set; }
        public Nullable<int> User_Id { get; set; }

 

    }


}