using ArticlProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace ArticlProject.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IDataHelper<Core.AuthorPost> dataHelperPost;
        public Core.AuthorPost authorPost;

        public ArticleModel(IDataHelper<Core.AuthorPost> dataHelperPost)
        {
            this.dataHelperPost = dataHelperPost;
            authorPost=new Core.AuthorPost();

        }

        public void OnGet()
        {
            var id = HttpContext.Request.RouteValues["id"];
            authorPost = dataHelperPost.Find(Convert.ToInt32( id));
        }
    }
}
