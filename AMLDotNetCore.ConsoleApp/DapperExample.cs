using AMLDotNetCore.ConsoleApp.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;";

        public void Read()
        {
            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "select * from tbl_blog where DeleteFlag = 0;";
            //    var lst = db.Query(query).ToList();

            //    foreach (var item in lst)
            //    {
            //        Console.Write(item.BlogId);
            //        Console.Write(item.BlogTitle);
            //        Console.Write(item.BlogAuthot);
            //        Console.Write(item.BlogContent);
            //    }
            //}
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDapperDataModel>(query).ToList();

                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthot);
                    Console.WriteLine(item.BlogContent);
                }
            }

        }
        public void Create(string title, string author, string content)
        {
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDapperDataModel
                {
                    BlogTitle = title,
                    BlogAuthot = author,
                    BlogContent = content,

                });
                Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");

            }

        }


    }
}
