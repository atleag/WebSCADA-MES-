using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.ViewModels
{
    public class FacilityManagement
    {
        public int ID { get; set; }

        public IEnumerable<facilities> Anlegg { get; set; }
    }
}