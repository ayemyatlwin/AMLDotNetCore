﻿using System;
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

        public void Create()
        {
            Console.WriteLine("Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();



            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                    ([BlogTitle]
                    ,[BlogAuthot]
                    ,[BlogContent]
                    ,[DeleteFlag])
              VALUES
                    (@BlogTitle
                    ,@BlogAuthot
                    ,@BlogContent
                    ,0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthot", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
        }

        public void Edit()
        {
            Console.Write("Blog Id: ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                   ,[BlogTitle]
                   ,[BlogAuthot]
                   ,[BlogContent]
                   ,[DeleteFlag]
               FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthot"]);
            Console.WriteLine(dr["BlogContent"]);
        }

        public void Update()
        {
            Console.WriteLine("Blog Id: ");
            string id = Console.ReadLine();

            Console.WriteLine("Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                       ,[BlogAuthot] = @BlogAuthot
                       ,[BlogContent] = @BlogContent
                       ,[DeleteFlag] = 0
                  WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthot", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
        }


    }
}
