using AMLDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp
{
    public class AdoDotNetExample2

    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService= new AdoDotNetService(_connectionString);
        }
        public void Read()
        {
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthot]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows) 
            {
                Console.WriteLine("Blog Title :" + dr["BlogTitle"]);
                Console.WriteLine("Blog Author :" + dr["BlogAuthot"]);
                Console.WriteLine("Blog Content :" + dr["BlogContent"]);
                Console.WriteLine("Blog Id :" + dr["BlogId"]);
                Console.WriteLine("--------------------------------------------------");
            }    


        }

        public void Edit() 
        {
            Console.Write("Blog Id - ");
            string id=Console.ReadLine()!;
            string query = @"SELECT [BlogId]
                               ,[BlogTitle]
                               ,[BlogAuthot]
                               ,[BlogContent]
                               ,[DeleteFlag]
                           FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";
            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{

            //    Name = "@BlogId",
            //    Value = id
            //};
            //var dt= _adoDotNetService.Query(query,sqlParameters);
            var dt= _adoDotNetService.Query(query, new SqlParameterModel
            {
               Name="@BlogId",
               Value=id!
            });
            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthot"]);
            Console.WriteLine(dr["BlogContent"]);
        }

        public void Create()
        {
            Console.WriteLine("Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string content = Console.ReadLine();


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


            int result = _adoDotNetService.Execute(query,

            new SqlParameterModel("@BlogTitle", title),
            new SqlParameterModel("@BlogAuthot", author),
            new SqlParameterModel("@BlogContent", content));

            Console.WriteLine(result == 1 ? "Creating Successful." : "Creating Failed.");
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


            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                       ,[BlogAuthot] = @BlogAuthot
                       ,[BlogContent] = @BlogContent
                       ,[DeleteFlag] = 0
                  WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query,
                new SqlParameterModel("@BlogId", id),
                new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthot", author),
                new SqlParameterModel("@BlogContent", content));

            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
        }


        public void Delete()
        {
            Console.WriteLine("Blog Id:");
            string id = Console.ReadLine();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                     SET [DeleteFlag] = 1
                     WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query, new SqlParameterModel("@BlogId", id));

            Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");
        }

    }
}
