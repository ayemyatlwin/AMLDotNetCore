using AMLDotNetCore.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.Domain.Features.Blog
{
  
        public class BlogV2Service : IBlogService
        {
            private readonly AppDbContext _db;

            public BlogV2Service(AppDbContext db)
            {
                _db = db;
            }

            public List<TblBlog> GetBlogs()
            {
                var model = _db.TblBlogs.AsNoTracking().ToList();
                return model;

            }

            public TblBlog GetById(int id)
            {
                var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                return item;
            }

            public TblBlog CreateBlog(TblBlog blog)
            {
                _db.TblBlogs.Add(blog);
                _db.SaveChanges();
                return blog;
            }

            public TblBlog UpdateBlog(int id, TblBlog blog)
            {
                var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return null;
                }
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthot = blog.BlogAuthot;
                item.BlogContent = blog.BlogContent;

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return item;


            }

            public TblBlog patchlog(int id, TblBlog blog)
            {
                var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return null;
                }
                if (!string.IsNullOrEmpty(blog.BlogTitle))
                {
                    item.BlogTitle = blog.BlogTitle;
                }
                if (!string.IsNullOrEmpty(blog.BlogAuthot))
                {
                    item.BlogAuthot = blog.BlogAuthot;
                }
                if (string.IsNullOrEmpty(blog.BlogContent))
                {
                    item.BlogContent = blog.BlogContent;
                }
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return item;


            }

            public bool? DeleteBlog(int id)
            {
                var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return null;
                }

                _db.Remove(item).State = EntityState.Deleted;
                int result = _db.SaveChanges();
                return result > 0;

            }
        }

    
}
