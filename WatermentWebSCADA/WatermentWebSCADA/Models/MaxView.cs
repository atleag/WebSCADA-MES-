using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA.Models
{
    public class MaxView
    {
        public class MusicStoreViewModel
        {

            public IEnumerable<WatermentWebSCADA.Models.facilities> facilities { get; set; }
            public IEnumerable<WatermentWebSCADA.Models.country> countries { get; set; }
        }
    }
}