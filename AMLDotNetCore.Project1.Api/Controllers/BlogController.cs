﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMLDotNetCore.Project1.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(new BlogModel[]
			{
				new BlogModel { Title = "Blog 1", Author = "Author 1" },
				new BlogModel { Title = "Blog 2", Author = "Author 2" },
				new BlogModel { Title = "Blog 3", Author = "Author 3" }

			});
		}

	}

	public class BlogModel
	{
		public string Title { get; set; }
		public string Author { get; set; }
	}
}
