using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public DataTable GetProducts(IEnumerable<MealItem> foodItems)
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

        public void InsertFoodItem(string Code, DateTime dt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("InsertFoodItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar, 50));
                cmd.Parameters["@Code"].Value = Code;

                cmd.Parameters.Add(new SqlParameter("@dt", SqlDbType.DateTime));
                cmd.Parameters["@dt"].Value = dt;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
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
                cmd.Parameters["@Protein"].Value = product.Nutrients["Protein"];

                cmd.Parameters.Add(new SqlParameter("@Carbohydrate", SqlDbType.VarChar, 255));
                cmd.Parameters["@Carbohydrate"].Value = product.Nutrients["Carbohydrates"];

                cmd.Parameters.Add(new SqlParameter("@Fat", SqlDbType.VarChar, 255));
                cmd.Parameters["@Fat"].Value = product.Nutrients["Fat"];

                cmd.Parameters.Add(new SqlParameter("@Calories", SqlDbType.VarChar, 255));
                cmd.Parameters["@Calories"].Value = product.Nutrients["Calories"];

                cmd.Parameters.Add(new SqlParameter("@Alcohol", SqlDbType.VarChar, 255));
                cmd.Parameters["@Alcohol"].Value = product.Nutrients["Alcohol"];

                cmd.Parameters.Add(new SqlParameter("@TotalSugars", SqlDbType.VarChar, 255));
                cmd.Parameters["@TotalSugars"].Value = product.Nutrients["TotalSugars"];

                //cmd.Parameters.Add(new SqlParameter("@AlcoholicDrinkSize", SqlDbType.Int));
                //cmd.Parameters["@AlcoholicDrinkSize"].Value = product.AlcoholicDrinkSize;

                cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                cmd.Parameters["@userId"].Value = userId;

                cmd.Parameters.Add(new SqlParameter("@ProductType", SqlDbType.Int));
                cmd.Parameters["@ProductType"].Value = userId;

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
                cmd.Parameters["@Protein"].Value = product.Nutrients["Protein"];

                cmd.Parameters.Add(new SqlParameter("@Carbohydrate", SqlDbType.VarChar, 255));
                cmd.Parameters["@Carbohydrate"].Value = product.Nutrients["Carbohydrates"];

                cmd.Parameters.Add(new SqlParameter("@Fat", SqlDbType.VarChar, 255));
                cmd.Parameters["@Fat"].Value = product.Nutrients["Fat"];

                cmd.Parameters.Add(new SqlParameter("@Calories", SqlDbType.VarChar, 255));
                cmd.Parameters["@Calories"].Value = product.Nutrients["Calories"];

                cmd.Parameters.Add(new SqlParameter("@Alcohol", SqlDbType.VarChar, 255));
                cmd.Parameters["@Alcohol"].Value = product.Nutrients["Alcohol"];

                cmd.Parameters.Add(new SqlParameter("@TotalSugars", SqlDbType.VarChar, 255));
                cmd.Parameters["@TotalSugars"].Value = product.Nutrients["TotalSugars"];

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

            return "99-" + count.ToString();
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

        private DataTable CreateCodeTable(IEnumerable<MealItem> foodItems)
        {
            var table = new DataTable();
            table.Columns.Add("Food Code", typeof(String));
            foreach (MealItem foodItem in foodItems)
            {
                table.Rows.Add(foodItem.Code);
            }
            return table;
        }

    }
}
