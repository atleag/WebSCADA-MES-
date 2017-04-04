using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatermentWebSCADA.ViewModels
{
    public class DateTimeXAxisChartData
    {
        public static List<DateTimeXAxisChartData> GetRandom()
        {
            var data = new List<DateTimeXAxisChartData>();

            Random rnd = new Random();

            double yValue1 = 50;
            double yValue2 = 200;

            var date = new DateTime(2010, 1, 1);

            for (var i = 0; i < 200; i++)
            {
                yValue1 += rnd.NextDouble() * 10 - 5;
                yValue2 += rnd.NextDouble() * 10 - 5;

                data.Add(new DateTimeXAxisChartData(date, Math.Round(yValue1, 2), Math.Round(yValue2, 2)));

                date = date.AddDays(1);

            }

            return data;
        }

        public static List<DateTimeXAxisChartData> GetLineChartData()
        {
            var data = new List<DateTimeXAxisChartData>();

            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 1), 62, 46));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 2), 60, 40));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 3), 68, 62));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 4), 58, 65));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 5), 52, 60));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 6), 60, 36));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 7), 48, 70));

            return data;
        }

        public static List<DateTimeXAxisChartData> GetLineChartDataWithNullValues()
        {
            var data = new List<DateTimeXAxisChartData>();

            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 1), 62, 46, 36));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 2), 60, 40, 30));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 3), 68, 62, 22));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 4), 58, 65, 25));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 5), null, null, null));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 6), 60, 36, 16));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 7), 48, 70, 30));

            return data;
        }

        public static List<DateTimeXAxisChartData> GetAreaChartData()
        {
            var data = new List<DateTimeXAxisChartData>();

            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 1, 1), 56, 46));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 2, 1), -20, 40));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 3, 1), -32, 62));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 4, 1), 50, 65));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 5, 1), 40, 60));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 6, 1), 36, 36));
            data.Add(new DateTimeXAxisChartData(new DateTime(2010, 7, 1), 70, 70));

            return data;
        }

        public static List<DateTimeXAxisChartData> GetDateTimeAxisSampleChartData()
        {
            var data = new List<DateTimeXAxisChartData>();

            data.Add(new DateTimeXAxisChartData(new DateTime(2011, 1, 6), 70));
            data.Add(new DateTimeXAxisChartData(new DateTime(2011, 1, 8), 82));
            data.Add(new DateTimeXAxisChartData(new DateTime(2011, 1, 10), 85));
            data.Add(new DateTimeXAxisChartData(new DateTime(2011, 1, 12), 70));
            data.Add(new DateTimeXAxisChartData(new DateTime(2011, 1, 14), 65));
            data.Add(new DateTimeXAxisChartData(new DateTime(2011, 1, 16), 68));

            return data;
        }

        public DateTimeXAxisChartData(DateTime valueX, double valueY1)
        {
            this.ValueX = valueX;
            this.ValueY1 = valueY1;
        }

        public DateTimeXAxisChartData(DateTime valueX, double? valueY1, double? valueY2)
        {
            this.ValueX = valueX;
            this.ValueY1 = valueY1;
            this.ValueY2 = valueY2;
        }

        public DateTimeXAxisChartData(DateTime valueX, double? valueY1, double? valueY2, double? valueY3)
        {
            this.ValueX = valueX;
            this.ValueY1 = valueY1;
            this.ValueY2 = valueY2;
            this.ValueY3 = valueY3;
        }

        public DateTime ValueX { get; set; }
        public double? ValueY1 { get; set; }
        public double? ValueY2 { get; set; }
        public double? ValueY3 { get; set; }
    }

}