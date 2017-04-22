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
    public class EquipmentViewModels
    {
    }
    public class FacilityEquipmentVM
    {
        [Display(Name = "Tag")]
        public string Tag { get; set; } //Equipment

        [Display(Name = "SI unit")]
        public string SIUnits { get; set; }//Equipment

        [Display(Name = "Description")]
        public string Description { get; set; }//Equipment

        [Display(Name = "Last Calibration")]
        public Nullable<System.DateTime> LastCalibrated { get; set; } //Equipment

        [Display(Name = "Date of installation")]
        public Nullable<System.DateTime> InstallDate { get; set; }

        [Display(Name = "Type Spesification")]
        public string TypeSpecification { get; set; }

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }


        public int facilities_Id { get; set; }

    }
    public class EquipmentAddVM
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

        [Display(Name ="Date of last calibration")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> LastCalibrated { get; set; }

        [Display(Name = "Date of installation")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> InstallDate { get; set; }

        [Required]
        [Display(Name = "Type Spesification")]
        public string TypeSpecification { get; set; }

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required]
        [Display(Name ="Facilities in which the equipment is added")]
        public int facilities_Id { get; set; }
    }
}