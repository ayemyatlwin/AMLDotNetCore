﻿using AMLDotNetCore.DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().ToList();
            //var lst = _db.TblBlogs.AsNoTracking().ToList().Where(x => x.DeleteFlag == false);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId==id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }




        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog) 
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            //return Ok(blog);
            return Ok("Successfully Created!");

        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {

            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            item.BlogTitle=blog.BlogTitle;
            item.BlogAuthot = blog.BlogAuthot;
            item.BlogContent=blog.BlogContent;  

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();


            //return Ok(item);
            return Ok("Successfully Updated!");
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId==id);
            if (item is null) 
            {
                NotFound();
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthot)) 
            {
                item.BlogAuthot=blog.BlogAuthot;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent=blog.BlogContent;
            }
            _db.Entry(item).State=EntityState.Modified;
            _db.SaveChanges();
			//return Ok(item);
			return Ok("Successfully Updated!");
		}

        [HttpDelete ("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                NotFound();
            }
            //item.DeleteFlag = true;
            //_db.Entry(item).State = EntityState.Modified;
            var result = _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok("Successfully Deleted!");
        }
    }
}
