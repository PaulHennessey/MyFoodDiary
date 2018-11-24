using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Domain;
using MyFoodDiary.Domain.Extensions;

namespace MyFoodDiary.Data.Concrete
{
    public class MealMapper : IMealMapper
    {
        public IEnumerable<Meal> HydrateMeals(DataTable dataTable)
        {
            return from DataRow row in dataTable.Rows select new Meal
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
            };
        }

    }
}