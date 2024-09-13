using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();


// md => markdown

// C# <=> Database

//breakpoint => f9

//one line down => f10

// ADO.NET (module of .Net framework) => using here...
// Dapper (ORM)
// EFCore / Entity Framework (ORM)

//  C# <=> ORM <=> sql query

// nuget => package manager

// Ctrl + .

// max connection = 100
// 100 = 99

// 101
string connectionString = "Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;";


Console.WriteLine("Connection String:"+connectionString);

SqlConnection conection = new SqlConnection(connectionString);

Console.WriteLine("Connection Opening...");


conection.Open();

Console.WriteLine("Connection Opened");

Console.WriteLine("Connection Closing...");

string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthot]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

SqlCommand cmd = new SqlCommand(query,conection);

//SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//DataTable dt = new DataTable();

//adapter.Fill(dt);


SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(reader["BlogId"]);
    Console.WriteLine(reader["BlogTitle"]);
    Console.WriteLine(reader["BlogAuthot"]);
    Console.WriteLine(reader["BlogContent"]);
}



conection.Close();



Console.WriteLine("Connection Closed");

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogId"]);
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthot"]);
//    Console.WriteLine(dr["BlogContent"]);


//}

Console.ReadKey();


