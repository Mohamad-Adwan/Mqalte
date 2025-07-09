using ArticlProject.Core;
using ArticlProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
namespace ArticlProject.Controllers

{
    [Authorize]

    public class AuthorPostController : Controller
    {
        private readonly IDataHelper<AuthorPost> datahelper;
        private readonly IDataHelper<Author> datahelperauthor;
        private readonly IDataHelper<Category> datahelpercategory;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly Code.FileHelper filehelper;
        private int PageItem;
        private Task<AuthorizationResult> result;
        private string UserId;

        public AuthorPostController(
            IDataHelper<AuthorPost> datahelper,
            IDataHelper<Author> datahelperauthor,
            IDataHelper<Category> datahelpercategory,
            IWebHostEnvironment webHost,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.datahelper = datahelper;
            this.datahelperauthor = datahelperauthor;
            this.datahelpercategory = datahelpercategory;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            filehelper = new Code.FileHelper(this.webHost);
            PageItem = 10;
           
        }
        // GET: AuthorPostController
        public ActionResult Index(int?id)
        {
            SetUser();
            if (result.Result.Succeeded)
            {
                //when admin see all
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
            else {
                //User =>UserId
                if (id == 0 || id == null)
                {
                    return View(datahelper.GetDataByUser(UserId).Take(PageItem));
                }
                else
                {
                    var data = datahelper.GetDataByUser(UserId).Where(x => x.Id > id).Take(PageItem);
                    return View(data);

                }

            }



        }
        // GET: AuthorPostController
        public ActionResult Search(string SearchItem)
        {
            SetUser();
            if (result.Result.Succeeded)
            {
                if (SearchItem == null) { return View("index", datahelper.GetAllData()); }
                else
                {
                    return View("index", datahelper.Serch(SearchItem));
                }
            }
            else {
                if (SearchItem == null) { return View("index", datahelper.GetDataByUser(UserId)); }
                else
                {
                    return View("index", datahelper.Serch(SearchItem).Where(x=>x.UserId==UserId).ToList());
                }
            }
              
        }
        // GET: AuthorPostController/Details/5
        public ActionResult Details(int id)
        {
            SetUser();
            return View(datahelper.Find(id));
        }

        // GET: AuthorPostController/Create
        public ActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: AuthorPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoreView.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost
                {
                    
                    Author = collection.Author,
                    AuthorId = datahelperauthor.GetAllData().Where(x=>x.UserId==UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CatrgoryId = datahelpercategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = datahelperauthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    AddedTime = DateTime.Now,
                    UserName = datahelperauthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostCategory   ,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    PostImageUrl = filehelper.UplodeFile(collection.PostImageUrl, "Images"),

                };
                datahelper.Add(Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorPostController/Edit/5
        public ActionResult Edit(int id)
        {
            var authpost=datahelper.Find(id);
            CoreView.AuthorPostView authorPostView = new CoreView.AuthorPostView
            {
                    Id = authpost.Id,
                    Author = authpost.Author,
                    AuthorId = authpost.AuthorId,
                    Category = authpost.Category,
                    CatrgoryId = authpost.CatrgoryId,
                    FullName = authpost.FullName,
                    AddedTime = authpost.AddedTime,
                    UserName = authpost.UserName,
                    PostCategory = authpost.PostCategory,
                    PostDescription = authpost.PostCategory,
                    PostTitle = authpost.PostTitle,
                    UserId = authpost.UserId,

            };
            return View(authorPostView);
        }

        // POST: AuthorPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CoreView.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost
                {
                    Id = collection.Id,
                    Author = collection.Author,
                    AuthorId = datahelperauthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CatrgoryId = datahelpercategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = datahelperauthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    AddedTime = DateTime.Now,
                    UserName = datahelperauthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostCategory,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    PostImageUrl = filehelper.UplodeFile(collection.PostImageUrl, "Images"),
                   

                };
                datahelper.Edit(id, Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorPostController/Delete/5
        public ActionResult Delete(int id)
        {
           
            return View(datahelper.Find(id));
        }

        // POST: AuthorPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CoreView.AuthorPostView collection)
        {
            try
            {
                datahelper.Delete(id);
                string filepath = "~/Images/" + collection.PostImageUrl;
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
        private void SetUser()
        {
            result = authorizationService.AuthorizeAsync(User, "Admin");
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
    
}
