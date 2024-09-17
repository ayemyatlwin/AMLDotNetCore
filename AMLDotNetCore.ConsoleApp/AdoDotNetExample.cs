using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        public readonly string _connectionString="Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;";

        public void Read()
        {
            Console.WriteLine("Connection string: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection opening...");


            connection.Open();

            Console.WriteLine("Connection Opened");

            Console.WriteLine("Connection Closing...");

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthot]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            adapter.Fill(dt);


            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader["BlogId"]);
            //    Console.WriteLine(reader["BlogTitle"]);
            //    Console.WriteLine(reader["BlogAuthot"]);
            //    Console.WriteLine(reader["BlogContent"]);
            //}



            connection.Close();



            Console.WriteLine("Connection Closed");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthot"]);
                Console.WriteLine(dr["BlogContent"]);


            }


        }

       
    }
}
