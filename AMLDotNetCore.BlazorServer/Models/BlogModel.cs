﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMLDotNetCore.BlazorServer.Models
{
	[Table("Tbl_Blog")]
	public class BlogModel
	{
        [Key]
        public int BlogId { get; set; }
		public string BlogTitle { get; set; }

		public string BlogContent { get; set; }

		public string BlogAuthot { get; set; }

		public bool DeleteFlag { get; set; }

	}


}
