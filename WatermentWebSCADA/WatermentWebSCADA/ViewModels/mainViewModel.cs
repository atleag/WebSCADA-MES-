using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA;

namespace WatermentWebSCADA.ViewModels
{
    public class MainViewModel
    {
      //  public FacilityViewModel facilitiess { get; set; }


        //facilities
        public string Name { get; set; }
        public string IP { get; set; }
        public string Domain { get; set; }
        //locations model
        public string Address { get; set; }
        public int Postcode { get; set; }
        public string County { get; set; }
    }
}