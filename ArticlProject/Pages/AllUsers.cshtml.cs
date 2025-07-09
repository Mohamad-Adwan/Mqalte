using ArticlProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ArticlProject.Pages
{
    public class AllUsersModel : PageModel
    {
        public readonly int NumberOfPost;
        private readonly IDataHelper<Core.Author> dataHelper;

        public AllUsersModel(
            IDataHelper<Core.Author> dataHelper

            )
        {
           
            this.dataHelper = dataHelper;
            NumberOfPost = 6;
            ListOfAuthor = new List<Core.Author>();
            this.dataHelper = dataHelper;
        }
        public List<Core.Author> ListOfAuthor { get; set; }

        public void OnGet(string LoadStates,  string search, int id)
        {
            if (LoadStates == null || LoadStates == "All")
            {
                AllAuthor();
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
                GetNextData(id - NumberOfPost);
            }
        }

     

        private void AllAuthor()
        {
            ListOfAuthor = dataHelper.GetAllData().Take(NumberOfPost).ToList();

        }
       
        private void SearchData(string SearchItem)
        {
            ListOfAuthor = dataHelper.Serch(SearchItem).ToList();

        }
        private void GetNextData(int id)
        {
            ListOfAuthor = dataHelper.GetAllData().Where(x => x.Id == id).Take(NumberOfPost).ToList();

        }
    }
}
