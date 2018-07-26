using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class LineChartViewModel
    {
        public string ChartTitle { get; set; }
        public List<string> BarNames { get; set; }
        public List<decimal> BarData { get; set; }
    }
}