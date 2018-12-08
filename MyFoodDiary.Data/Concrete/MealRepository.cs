using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Linq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Data.Concrete
{
    public class MealRepository : IMealRepository
    {
        private readonly string _connectionString;

        public MealRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// This is used by the Search.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetMeals(int userId)
        {
          var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetMeals", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                command.Parameters["@userId"].Value = userId;

                var da = new SqlDataAdapter(command);
                da.Fill(dataTable);
            }

            return dataTable;
        }


        ///// <summary>
        ///// Note that the stored procedure will ignore duplicate fooditems, and
        ///// only return a single product.
        ///// </summary>
        ///// <param name="foodItems"></param>
        ///// <returns></returns>
        //public DataTable GetProducts(IEnumerable<FoodItem> foodItems)
        //{
        //    var dataTable = new DataTable();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand command = new SqlCommand("GetProducts", connection);
        //        command.CommandType = CommandType.StoredProcedure;

        //        command.Parameters.Add(new SqlParameter("@Food_Codes", SqlDbType.Structured));
        //        command.Parameters["@Food_Codes"].Value = CreateCodeTable(foodItems);

        //        SqlDataAdapter da = new SqlDataAdapter(command);
        //        da.Fill(dataTable);
        //    }

        //    return dataTable;
        //}

        ///// <summary>
        ///// This is used by the ProductsController Index.
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public DataTable GetCustomProducts(int userId)
        //{
        //    var dataTable = new DataTable();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var command = new SqlCommand("GetCustomProducts", connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
        //        command.Parameters["@userId"].Value = userId;

        //        var da = new SqlDataAdapter(command);
        //        da.Fill(dataTable);
        //    }

        //    return dataTable;
        //}

        public void CreateMeal(Meal meal, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("InsertMeal", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int));
                cmd.Parameters["@UserId"].Value = userId;

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255));
                cmd.Parameters["@Name"].Value = meal.Name;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateMeal(Meal meal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UpdateMeal", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                cmd.Parameters["@Id"].Value = meal.Id;

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255));
                cmd.Parameters["@Name"].Value = meal.Name;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }




        //public void DeleteProduct(string code)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var cmd = new SqlCommand("DeleteProduct", connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar, 255));
        //        cmd.Parameters["@Code"].Value = code;

        //        connection.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}


        //private string GenerateCode(int userId)
        //{
        //    var dataTable = new DataTable();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var command = new SqlCommand("GetCustomProductCount", connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
        //        command.Parameters["@userId"].Value = userId;

        //        var da = new SqlDataAdapter(command);
        //        da.Fill(dataTable);
        //    }

        //    DataRow row = dataTable.Rows[0];
        //    int count = Convert.ToInt32(row["ProductCount"]);
        //    count++;

        //    return "99-" + userId.ToString() + "-" + count.ToString();
        //}

        public DataTable GetMeal(int id)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetMeal", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
            }

            return dataTable;
        }


        //private DataTable CreateCodeTable(IEnumerable<FoodItem> foodItems)
        //{
        //    var table = new DataTable();
        //    table.Columns.Add("Food Code", typeof(String));
        //    foreach (FoodItem foodItem in foodItems)
        //    {
        //        table.Rows.Add(foodItem.Code);
        //    }
        //    return table;
        //}

    }
}
