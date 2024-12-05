// See https://aka.ms/new-console-template for more information
using AMLDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");

//HttpClientExample example = new HttpClientExample();
//await example.ReadAsync();
//await example.EditAsync(101);
//await example.CreateAsync(1, " title", "testing body");
//await example.updateAsync(1, 1, " title edit ", "testing body edit");
//await example.deleteAsync(3);

//RestClientExample restClientExample = new RestClientExample();
//await restClientExample.ReadAsync();
//await restClientExample.CreateAsync(1, " title", "testing body");
//await restClientExample.EditAsync(10);
//await restClientExample.updateAsync(1, 1, " title edit ", "testing body edit");
//await restClientExample.deleteAsync(3);

Console.Write("waiting for api...");
Console.ReadLine();

RefitExample refitExample = new RefitExample();
await refitExample.Run();

Console.ReadLine();