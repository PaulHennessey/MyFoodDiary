using System;
using System.Data;

namespace MyFoodDiary.Domain.Extensions
{
    public static class Extensions
    {
        public static double GetValue(this DataRow row, string columnName)
        {
            double value = 0.0;

            if (row.IsNull(columnName))
            {
                return value;
            }
            else
            {
                Double.TryParse(row[columnName].ToString(), out value);
                return value;
            }
        }
    }
}
