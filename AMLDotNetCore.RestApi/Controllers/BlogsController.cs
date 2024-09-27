using AMLDotNetCore.DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db= new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().ToList();
            //var lst = _db.TblBlogs.AsNoTracking().ToList().Where(x => x.DeleteFlag == false);
            return Ok(new { message = "success",resqData=lst });
        }

        [HttpPost]
        public IActionResult CreateBlog()
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBlog()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBlog()
        {
            return Ok();
        }
    }
}
