using AMLDotNetCore.DataBase.Models;
using AMLDotNetCore.Domain.Features.Blog;
using AMLDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMLDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
           var lst = _blogService.GetBlogs();
            return View(lst);
        }

        [ActionName("Create")]
		public IActionResult CreateBlog()
		{
			return View("CreateBlog");
		}

		[ActionName("Save")]
		public IActionResult SaveBlog(BlogRequestModel requestModel)
        {
            try
            {
				_blogService.CreateBlog(new TblBlog
				{
					BlogAuthot = requestModel.Author,
					BlogContent = requestModel.Content,
					BlogTitle = requestModel.Title
				});

                TempData["IsSuccess"] = true;
                TempData["Message"] = " Blog Created Successfullly!";

			}
			catch(Exception ex)
            {
				TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();

			}
            return RedirectToAction("Index");

        }


		[ActionName("Delete")]
		public IActionResult DeleteBlog(int id)

		{
			_blogService.DeleteBlog(id);
			return RedirectToAction("Index");
		}

		[ActionName("Edit")]
		public IActionResult EditBlog(int id)

		{
			var blog = _blogService.GetById(id);
			BlogRequestModel model = new BlogRequestModel
			{
				Id= blog.BlogId,
				Title = blog.BlogTitle,
				Content = blog.BlogContent,
				Author = blog.BlogAuthot,
			};
			return View("EditBlog", model);
		}

		[HttpPost]
		[ActionName("Update")]
		public IActionResult UpdateBlog(int id , BlogRequestModel requestModel)
		{
			try
			{
				_blogService.UpdateBlog(id,new TblBlog
				{
					BlogAuthot = requestModel.Author,
					BlogContent = requestModel.Content,
					BlogTitle = requestModel.Title
				});

				TempData["IsSuccess"] = true;
				TempData["Message"] = " Blog Updated Successfullly!";

			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();

			}
			return RedirectToAction("Index");

		}

	}
}
