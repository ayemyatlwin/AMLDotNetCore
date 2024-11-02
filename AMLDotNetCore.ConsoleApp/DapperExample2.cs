using AMLDotNetCore.ConsoleApp.Models;
using AMLDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine("Blog Id :" + item.BlogId);
                Console.WriteLine("Blog Title :" + item.BlogTitle);
                Console.WriteLine("Blog Author :" + item.BlogAuthot);
                Console.WriteLine("Blog Content :" + item.BlogContent);
                Console.WriteLine("--------------------------------------------------");
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

            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthot = author,
                BlogContent = content,
            });
            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Fail.");
        }

        public void Edit(int id)
        {
            string query = @"select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = id,
            });

            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthot);
            Console.WriteLine(item.BlogContent);
        }

        public void Update(int id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                       ,[BlogAuthot] = @BlogAuthot
                       ,[BlogContent] = @BlogContent
                       ,[DeleteFlag] = 0
                  WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthot = author,
                BlogContent = content,
            });
            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Fail.");
        }


        public void Delete(int id)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                            SET [DeleteFlag] = 1
                            WHERE BlogId = @BlogId";
            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = id,
            });
            Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Fail.");
        }

    }
}
