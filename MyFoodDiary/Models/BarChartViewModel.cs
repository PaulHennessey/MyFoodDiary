using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class BarChartViewModel
    {
        public string ChartTitle { get; set; }
        public List<string> BarNames { get; set; }
        public List<double> BarData { get; set; }
    }
}