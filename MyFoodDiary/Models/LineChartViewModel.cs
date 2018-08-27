using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class LineChartViewModel
    {
        public List<string> ChartTitle { get; set; }
        public List<string> BarNames { get; set; }
        public List<List<decimal>> BarData { get; set; }
    }
}