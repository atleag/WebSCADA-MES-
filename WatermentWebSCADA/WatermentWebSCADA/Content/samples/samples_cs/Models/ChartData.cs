using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamplesBrowser.Models
{
    public class ChartData
    {
        public static List<ChartData> GetData()
        {
            var data = new List<ChartData>();

            data.Add(new ChartData("A", 46, 78));
            data.Add(new ChartData("B", 35, 72));
            data.Add(new ChartData("C", 68, 86));
            data.Add(new ChartData("D", 30, 23));
            data.Add(new ChartData("E", 27, 70));
            data.Add(new ChartData("F", 85, 60));
            data.Add(new ChartData("D", 43, 88));
            data.Add(new ChartData("H", 29, 22));

            return data;
        }

        public ChartData(string label, double value1, double value2)
        {
            this.Label = label;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public string Label { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
    }
}