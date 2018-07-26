using System;
using System.Data;

namespace MyFoodDiary.Domain.Extensions
{
    public static class Extensions
    {
        public static decimal GetValue(this DataRow row, string columnName)
        {
            decimal value = 0.0m;

            if (row.IsNull(columnName))
            {
                return value;
            }
            else
            {
                Decimal.TryParse(row[columnName].ToString(), out value);
                return value;
            }
        }
    }
}
