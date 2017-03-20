using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatermentWebSCADA.Models
{
    public class MultiView
    {
        public List<country> GetCountry { get; set; }
        public List<facilities> GetFacilities { get; set; }
        public IEnumerable<country> countriess { get; set; }
    }
}