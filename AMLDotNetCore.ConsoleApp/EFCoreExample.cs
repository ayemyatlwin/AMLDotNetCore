﻿using AMLDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where( x=> x.DeleteFlag==false ).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthot);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthot = author,
                BlogContent = content,
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");


        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Matching result...");
                return;

            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthot);
            Console.WriteLine(item.BlogContent);



        }

        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Matching result...");
                return;

            }
            if (!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthot = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }
            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Updated Data" : "Update Failed");


        }


        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Matching result...");
                return;

            }
            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Deleted Data" : "Delete Failed");

        }

        // ---- by updating delete flag ------//

        //public void Delete(int id)
        //{
        //    AppDbContext db = new AppDbContext();
        //    var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

        //    if (item is null)
        //    {
        //        Console.WriteLine("No Matching result...");
        //        return;

        //    }
        //    item.DeleteFlag=true;
        //    db.Entry(item).State = EntityState.Modified;
        //    var result = db.SaveChanges();
        //    Console.WriteLine(result == 1 ? "Deleted Data" : "Delete Failed");

        //}
    }
}
