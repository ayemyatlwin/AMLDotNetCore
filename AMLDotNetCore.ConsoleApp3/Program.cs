// See https://aka.ms/new-console-template for more information
using AMLDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");

HttpClientExample example = new HttpClientExample();
await example.ReadAsync();
