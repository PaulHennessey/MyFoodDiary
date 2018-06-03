using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Domain;
using MyFoodDiary.Domain.Extensions;

namespace MyFoodDiary.Data.Concrete
{
    public class ProductMapper : IProductMapper
    {
        public IEnumerable<Product> HydrateProducts(DataTable dataTable)
        {
            return from DataRow row in dataTable.Rows select new Product
            {
                Code = row["Code"].ToString(),
                Name = row["Name"].ToString(),
                Nutrients = new Dictionary<string, double>()
                {
                    { "Protein", row.GetValue("Protein") },
                    { "Carbohydrates", row.GetValue("Carbohydrate") },
                    { "Fat", row.GetValue("Fat") },
                    { "Calories", row.GetValue("Calories") },
                    { "Alcohol", row.GetValue("Alcohol") },
                    { "TotalSugars", row.GetValue("TotalSugars") }
                },

                //ServingSize = 1
                //ServingSize = Convert.ToInt32(row.GetValue("ServingSize"))
            };
        }
    }
}
