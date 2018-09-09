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
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// This is used by the Search.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetProducts(int userId)
        {
          var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetAllProducts", connection)
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


        /// <summary>
        /// Note that the stored procedure will ignore duplicate fooditems, and
        /// only return a single product.
        /// </summary>
        /// <param name="foodItems"></param>
        /// <returns></returns>
        public DataTable GetProducts(IEnumerable<FoodItem> foodItems)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("GetProducts", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Food_Codes", SqlDbType.Structured));
                command.Parameters["@Food_Codes"].Value = CreateCodeTable(foodItems);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
            }

            return dataTable;
        }

        /// <summary>
        /// This is used by the ProductsController Index.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetCustomProducts(int userId)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetCustomProducts", connection)
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

        public void CreateProduct(Product product, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("InsertProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar, 255));
                cmd.Parameters["@Code"].Value = GenerateCode(userId);

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255));
                cmd.Parameters["@Name"].Value = product.Name;

                cmd.Parameters.Add(new SqlParameter("@Protein", SqlDbType.VarChar, 255));
                cmd.Parameters["@Protein"].Value = product.ProductMacronutrients.Quantity("Protein");

                cmd.Parameters.Add(new SqlParameter("@Carbohydrate", SqlDbType.VarChar, 255));                
                cmd.Parameters["@Carbohydrate"].Value = product.ProductMacronutrients.Quantity("Carbohydrates");

                cmd.Parameters.Add(new SqlParameter("@Fat", SqlDbType.VarChar, 255));
                cmd.Parameters["@Fat"].Value = product.ProductMacronutrients.Quantity("Fat");

                cmd.Parameters.Add(new SqlParameter("@Calories", SqlDbType.VarChar, 255));                
                cmd.Parameters["@Calories"].Value = product.ProductMacronutrients.Quantity("Calories");

                cmd.Parameters.Add(new SqlParameter("@Alcohol", SqlDbType.VarChar, 255));
                cmd.Parameters["@Alcohol"].Value = product.ProductMacronutrients.Quantity("Alcohol");

                cmd.Parameters.Add(new SqlParameter("@TotalSugars", SqlDbType.VarChar, 255));               
                cmd.Parameters["@TotalSugars"].Value = product.ProductMacronutrients.Quantity("Total Sugars");

                cmd.Parameters.Add(new SqlParameter("@Calcium", SqlDbType.VarChar, 255));
                cmd.Parameters["@Calcium"].Value = product.ProductMicronutrients.Quantity("Calcium");

                cmd.Parameters.Add(new SqlParameter("@VitaminD", SqlDbType.VarChar, 255));
                cmd.Parameters["@VitaminD"].Value = product.ProductMicronutrients.Quantity("Vitamin D");

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UpdateProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar, 255));
                cmd.Parameters["@Code"].Value = product.Code;

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255));
                cmd.Parameters["@Name"].Value = product.Name;

                cmd.Parameters.Add(new SqlParameter("@Protein", SqlDbType.VarChar, 255));
                cmd.Parameters["@Protein"].Value = product.ProductMacronutrients.Quantity("Protein");

                cmd.Parameters.Add(new SqlParameter("@Carbohydrate", SqlDbType.VarChar, 255));
                cmd.Parameters["@Carbohydrate"].Value = product.ProductMacronutrients.Quantity("Carbohydrates");

                cmd.Parameters.Add(new SqlParameter("@Fat", SqlDbType.VarChar, 255));
                cmd.Parameters["@Fat"].Value = product.ProductMacronutrients.Quantity("Fat");

                cmd.Parameters.Add(new SqlParameter("@Calories", SqlDbType.VarChar, 255));
                cmd.Parameters["@Calories"].Value = product.ProductMacronutrients.Quantity("Calories");

                cmd.Parameters.Add(new SqlParameter("@Alcohol", SqlDbType.VarChar, 255));
                cmd.Parameters["@Alcohol"].Value = product.ProductMacronutrients.Quantity("Alcohol");

                cmd.Parameters.Add(new SqlParameter("@TotalSugars", SqlDbType.VarChar, 255));
                cmd.Parameters["@TotalSugars"].Value = product.ProductMacronutrients.Quantity("Total Sugars");

                cmd.Parameters.Add(new SqlParameter("@Calcium", SqlDbType.VarChar, 255));
                cmd.Parameters["@Calcium"].Value = product.ProductMicronutrients.Quantity("Calcium");

                cmd.Parameters.Add(new SqlParameter("@VitaminD", SqlDbType.VarChar, 255));
                cmd.Parameters["@VitaminD"].Value = product.ProductMicronutrients.Quantity("Vitamin D");

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void DeleteProduct(string code)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DeleteProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar, 255));
                cmd.Parameters["@Code"].Value = code;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }


        private string GenerateCode(int userId)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetCustomProductCount", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                command.Parameters["@userId"].Value = userId;

                var da = new SqlDataAdapter(command);
                da.Fill(dataTable);
            }

            DataRow row = dataTable.Rows[0];
            int count = Convert.ToInt32(row["ProductCount"]);
            count++;

            return "99-" + userId.ToString() + "-" + count.ToString();
        }

        public DataTable GetProduct(string code)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar, 255));
                command.Parameters["@Code"].Value = code;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
            }

            return dataTable;
        }

        private DataTable CreateCodeTable(IEnumerable<FoodItem> foodItems)
        {
            var table = new DataTable();
            table.Columns.Add("Food Code", typeof(String));
            foreach (FoodItem foodItem in foodItems)
            {
                table.Rows.Add(foodItem.Code);
            }
            return table;
        }

    }
}
