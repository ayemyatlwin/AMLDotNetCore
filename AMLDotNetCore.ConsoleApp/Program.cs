using AMLDotNetCore.ConsoleApp;
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


//AdoDotNet-CRUD
AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

//Dapper-CRUD
DapperExample dapperExample= new DapperExample();
//dapperExample.Read();
//dapperExample.Create("Learning", "AML", "Learing Content");
dapperExample.Edit(1011);
dapperExample.Edit(1012);
dapperExample.Update(1011, "Learning-Update", "AML", "Learing Content Update");





Console.ReadKey();


