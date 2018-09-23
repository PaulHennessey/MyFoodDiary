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
    public class TrackablesRepository : ITrackablesRepository
    {
        private readonly string _connectionString;

        public TrackablesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetTrackables(int userId)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetTrackables", connection)
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

        public DataTable GetTrackable(int id)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetTrackable", connection)
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


        ///// <summary>
        ///// Note that the stored procedure will ignore duplicate fooditems, and
        ///// only return a single product.
        ///// </summary>
        ///// <param name="foodItems"></param>
        ///// <returns></returns>
        //public DataTable GetProducts(IEnumerable<Trackable> trackables)
        //{
        //    var dataTable = new DataTable();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand command = new SqlCommand("GetTrackables", connection);
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

        public void CreateTrackable(Trackable trackable, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("InsertTrackable", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int));
                cmd.Parameters["@UserId"].Value = userId;

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255));
                cmd.Parameters["@Name"].Value = trackable.Name;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTrackable(Trackable trackable)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UpdateTrackable", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                cmd.Parameters["@Id"].Value = trackable.Id;

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255));
                cmd.Parameters["@Name"].Value = trackable.Name;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTrackable(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DeleteTrackable", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                cmd.Parameters["@Id"].Value = id;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
