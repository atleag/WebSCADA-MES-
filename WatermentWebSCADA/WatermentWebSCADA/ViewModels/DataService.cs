
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.ViewModels
{
    public static class DataService
    {   
        public static List<measurements> TestVerdi { get; set; }
        public static List<DataPoint> GetRandomDataForNumericAxis(int count)
        {
            //using (var db = new Models.watermentdbEntities())
            //{
            //    var model = new MainViewModel
            //    {
            //        Verdier = db.measurements.Where(x => x.equipments_facilities_Id == 1).Where(i => i.equipments.Description == "Temperature Reactor").ToList()
          
            //    };
                
                
            //    }


            
            double y = 10;
            int yx = 0;
            float? ProcessVerdi = 0;
            double yy = 0;
           

            _dataPoints = new List<DataPoint>();

            //ProcessVerdi = TestVerdi[1].ProcessValue;
            for (int i = 0; i < count; i++)
            {
                //ProcessVerdi = TestVerdi[i].ProcessValue;

                //y = y + (random.Next(0, 20) - 10);
                ProcessVerdi = 21;
                y = i + i;
             //y = Convert.ToDouble(TestVerdi[i].ProcessValue);
                yy = y;

                _dataPoints.Add(new DataPoint(i, y));
            }
            //_dataPoints.Add(new DataPoint(1, 100));
            return _dataPoints;
        }

        public static List<DataPoint> GetRandomDataForCategoryAxis(int count)
        {
            double y = 50;
            DateTime dateTime = new DateTime(2006, 01, 1, 0, 0, 0);
            string label = "";

            _dataPoints = new List<DataPoint>();


            for (int i = 0; i < count; i++)
            {
                y = y + (random.Next(0, 20) - 10);
                label = dateTime.ToString("dd MMM");

                _dataPoints.Add(new DataPoint(y, label));
                dateTime = dateTime.AddDays(1);
            }

            return _dataPoints;
        }

        public static List<DataPoint> GetRandomDataForDateTimeAxis(int count)
        {
            double x = 0;
            double y = 50;

            var dateTime = new DateTime(2006, 01, 10, 0, 0, 0, DateTimeKind.Local);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            _dataPoints = new List<DataPoint>();


            for (int i = 0; i < count; i++)
            {
                y = y + (random.Next(0, 20) - 10);

                x = dateTime.ToUniversalTime().Subtract(epoch).TotalMilliseconds;


                _dataPoints.Add(new DataPoint(x, y));
                dateTime = dateTime.AddDays(1);
                //dateTime = dateTime.AddHours(1);
            }

            return _dataPoints;
        }

        private static Random random = new Random(DateTime.Now.Millisecond);
        private static List<DataPoint> _dataPoints;
    }
}