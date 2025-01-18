using AMLDotNetCore.DataBase.Models;
using AMLDotNetCore.Domain.Features.Blog;
using AMLDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using static AMLDotNetCore.MvcApp.Controllers.BlogAjaxController;

namespace AMLDotNetCore.MvcApp.Controllers
{
	public class BlogAjaxController : Controller
	{
		public readonly IBlogService _blogService;

		public BlogAjaxController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		[ActionName("Index")]
		public IActionResult BlogList()
		{
			var lst = _blogService.GetBlogs();
			return View("BlogList", lst);
		}

		[ActionName("List")]
		public IActionResult BlogAjaxList()
		{
			var lst = _blogService.GetBlogs();
			return Json(lst);
		}
		
		
		[ActionName("Create")]
		public IActionResult CreateBlog()
		{
			return View("CreateBlog");
		}

		[ActionName("Edit")]
		public IActionResult EditBlog(int id)
		{
			var blog = _blogService.GetById(id);
			BlogRequestModel model = new BlogRequestModel
			{
				Id = blog.BlogId,
				Title = blog.BlogTitle,
				Content = blog.BlogContent,
				Author = blog.BlogAuthot,
			};
			return View("EditBlog", model);
		}


		[ActionName("Save")]
		public IActionResult SaveBlog(BlogRequestModel requestModel)
		{
			MessageModel messageModel;
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
				messageModel = new MessageModel(true, "Blog Created Successfully");
			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();
				messageModel = new MessageModel(false,ex.ToString());

			}
			return Json(messageModel);

		}


		[ActionName("Update")]

		public IActionResult Updateblog(int id, BlogRequestModel requestModel)
		{
			MessageModel messageModel;
			try
			{
				_blogService.UpdateBlog(id, new TblBlog
				{
					BlogAuthot = requestModel.Author,
					BlogContent = requestModel.Content,
					BlogTitle = requestModel.Title
				});
				TempData["IsSuccess"] = true;
				TempData["Message"] = "Blog Updatd Succcessfully!";
				messageModel = new MessageModel(true, "Blog Updatd Succcessfully!");

			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();
				messageModel = new MessageModel(false, ex.ToString());
			}
			return Json(messageModel);
		}


		[ActionName("Delete")]

		public IActionResult DeleteBlog( BlogRequestModel requestModel)
		{
			MessageModel messageModel;
			try
			{
				_blogService.DeleteBlog(requestModel.Id);
				TempData["IsSuccess"] = true;
				TempData["Message"] = "Blog Deleted Succcessfully!";
				messageModel = new MessageModel(true, "Blog Deleted Succcessfully!");

			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();
				messageModel = new MessageModel(false, ex.ToString());
			}
			return Json(messageModel);
		}

		public class MessageModel
			{

				 public MessageModel() { }

				public MessageModel(bool isSuccess,string message)
				{
				IsSuccess = isSuccess;
				Message = message;
				
				}
				public bool IsSuccess {  get; set; }
			     public string Message { get; set; }
			}


		
	}
}
