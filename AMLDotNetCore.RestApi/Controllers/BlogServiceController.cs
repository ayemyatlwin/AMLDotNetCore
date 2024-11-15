using AMLDotNetCore.DataBase.Models;
using AMLDotNetCore.Domain.Features.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogService _service;

        public BlogServiceController() 
        {
            _service = new BlogService();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _service.GetBlogs();
            return Ok(new { message = "success", data = lst });
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _service.GetById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", data = item });
        }


        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
           _service.CreateBlog(blog);
            return Ok(new { message = "success", data = blog });
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {

            var item = _service.UpdateBlog(id, blog);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(new { message = "success", resqData = item });
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _service.patchlog(id, blog);
            if (item is null)
            {
                NotFound();
            }
            return Ok(new { message = "success", resqData = item });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _service.DeleteBlog(id);
            if (item is null)
            {
                NotFound();
            }
           
            return Ok();
        }
    }
}
