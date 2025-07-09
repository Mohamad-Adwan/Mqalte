using ArticlProject.Data;
using ArticlProject.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Linq;
using System;



namespace ArticlProject.Pages
{
    [Authorize]

    public class AdminIndexModel : PageModel
    {
        private readonly IDataHelper<ArticlProject.Core.AuthorPost> datahelper;

        public AdminIndexModel(IDataHelper<ArticlProject.Core.AuthorPost> datahelper)
        {
            this.datahelper = datahelper;
        }
        public int AllPost { get; set; }
        public int PostLastMonth { get; set; }
        public int PostThisYear { get; set; }
        public string Name { get; set; }


        public void OnGet()
        {
            var datam=DateTime.Now.AddMonths(-1);
            var datay = DateTime.Now.AddYears(-1);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            AllPost =datahelper.GetDataByUser(userId).Count();
            PostLastMonth = datahelper.GetDataByUser(userId).Where(x=>x.AddedTime>= datam).Count();
            PostThisYear = datahelper.GetDataByUser(userId).Where(x => x.AddedTime >= datay).Count();

        }
    }
}
