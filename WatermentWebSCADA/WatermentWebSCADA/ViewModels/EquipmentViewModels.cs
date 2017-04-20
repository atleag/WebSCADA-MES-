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
        public string Tag { get; set; } //Equipment
        public string SIUnits { get; set; }//Equipment
        public string Description { get; set; }//Equipment
        public Nullable<System.DateTime> LastCalibrated { get; set; } //Equipment

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

        [Required]
        [Display(Name ="Facilities in which the equipment is added")]
        public int facilities_Id { get; set; }
    }
}