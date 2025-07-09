using ArticlProject.Core;
using ArticlProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
namespace ArticlProject.Controllers
{
    
    public class AuthorController : Controller
    {
        private readonly IDataHelper<Author> datahelper;
        private readonly IAuthorizationService authorizationService;
        private readonly IWebHostEnvironment webHost;
        private readonly Code.FileHelper filehelper;
        private int PageItem;

        public AuthorController(IDataHelper<Author>datahelper,
            IAuthorizationService authorizationService,
            IWebHostEnvironment webHost)
        {
            this.datahelper = datahelper;
            this.authorizationService = authorizationService;
            this.webHost=webHost;
            filehelper=new Code.FileHelper(this.webHost);
            PageItem = 10;
        }
        [Authorize("Admin")]
        // GET: AuthorController
        public ActionResult Index(int?id)
        {
            if (id == 0 || id == null)
            {
                return View(datahelper.GetAllData().Take(PageItem));
            }
            else
            {
                var data = datahelper.GetAllData().Where(x => x.Id > id).Take(PageItem);
                return View(data);

            }
        }
        [Authorize("Admin")]
        // GET: AuthorController
        public ActionResult Search(string SearchItem)
        {
            if (SearchItem == null) { return View("index", datahelper.GetAllData()); }
            else
            {
                return View("index", datahelper.Serch(SearchItem));
            }
        }
        // POST: AuthorController/Create'
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var author=datahelper.Find(id);
            CoreView.AuthorView authorView = new CoreView.AuthorView() {
                Id = author.Id,
                Bio = author.Bio,
                FaceBook = author.FaceBook,
                Twitter = author.Twitter,
                Instgram = author.Instgram,
                FullName = author.FullName,
                UserId = author.UserId,
                UserName = author.UserName,


            };
            return View(authorView);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, CoreView.AuthorView collection)
        {
            try
            {
                var author = new Author
                {
                    Id = collection.Id,
                    Bio = collection.Bio,
                    FaceBook = collection.FaceBook,
                    Twitter = collection.Twitter,
                    Instgram = collection.Instgram,
                    FullName = collection.FullName,
                    UserId = collection.UserId,
                    UserName = collection.UserName,
                    ProfileImgaeUrl = filehelper.UplodeFile(collection.ProfileImgaeUrl, "Images"),

                };
                datahelper.Edit(id, author);
                var result=authorizationService.AuthorizeAsync(User, "Admin");
                if (result.Result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return RedirectToAction("/Adminindex");

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        [Authorize("Admin")]
        public ActionResult Delete(int id)
        {
            //var author = datahelper.Find(id);
            //CoreView.AuthorView authorView = new CoreView.AuthorView()
            //{
            //    Id = author.Id,
            //    Bio = author.Bio,
            //    FaceBook = author.FaceBook,
            //    Twitter = author.Twitter,
            //    Instgram = author.Instgram,
            //    FullName = author.FullName,
            //    UserId = author.UserId,
            //    UserName = author.UserName,


            //};
            //return View(authorView);
            var auther = datahelper.Find(id);
            return View(auther);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public ActionResult Delete(int id, Author collection)
        {
            try
            {
                datahelper.Delete(id);
                string filepath = "~/Images/"+ collection.ProfileImgaeUrl;
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
              
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
