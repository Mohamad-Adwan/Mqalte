using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlProject.Core;
using ArticlProject.Data;

namespace ArticlProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<Core.Category> dataHelperCategory;
        private readonly IDataHelper<Core.AuthorPost> dataHelperPost;
        public  readonly int NumberOfPost;

        public IndexModel(ILogger<IndexModel> logger,
            IDataHelper<Core.Category> dataHelperCategory,
            IDataHelper<Core.AuthorPost> dataHelperPost

            )
        {
            _logger = logger;
            this.dataHelperCategory = dataHelperCategory;
            this.dataHelperPost = dataHelperPost;
            NumberOfPost = 6;
            ListOfCategory=new List<Core.Category>();
            ListOfPost =new List<Core.AuthorPost>();
        }
        public List<Core.Category> ListOfCategory { get; set; }
        public List<Core.AuthorPost> ListOfPost { get; set; }

        public void OnGet(string LoadStates,string CategoryName,string search,int id)
        {
            AllCategory();
            if (LoadStates == null || LoadStates == "All")
            {
                AllPost();
            }
            else if (LoadStates == "ByCategory")
            {
                PostByCaregory(CategoryName);
            }
            else if (LoadStates == "Search")
            {
                SearchData(search);
            }
            else if (LoadStates == "Next") 
            {
                GetNextData(id);
            }
            else if (LoadStates == "Prev")
            {
                GetNextData(id-NumberOfPost);
            }
        }

        private void AllCategory()
        {
            ListOfCategory =dataHelperCategory.GetAllData();

        }

        private void AllPost()
        {
            ListOfPost = dataHelperPost.GetAllData().Take(NumberOfPost).ToList();

        }
        private void PostByCaregory(string CategoryName)
        {
            ListOfPost = dataHelperPost.GetAllData().Where(x=>x.PostCategory==CategoryName).Take(NumberOfPost).ToList();

        }
        private void SearchData(string SearchItem)
        {
            ListOfPost = dataHelperPost.Serch(SearchItem).ToList();

        }
        private void GetNextData(int id)
        {
            ListOfPost = dataHelperPost.GetAllData().Where(x => x.Id == id).Take(NumberOfPost).ToList() ;

        }
    }
}
