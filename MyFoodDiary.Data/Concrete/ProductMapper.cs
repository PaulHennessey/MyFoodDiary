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
                Nutrients = new Dictionary<string, decimal>()
                {
                    { "Calories", row.GetValue("Calories") },
                    { "Fat", row.GetValue("Fat") },
                    { "Carbohydrates", row.GetValue("Carbohydrate") },
                    { "TotalSugars", row.GetValue("TotalSugars") },
                    { "Protein", row.GetValue("Protein") },                   
                    { "Alcohol", row.GetValue("Alcohol") },
                    { "Cholesterol", row.GetValue("Cholesterol") }
                },
            };
        }
    }
}