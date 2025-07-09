using ArticlProject.Core;
using ArticlProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ArticlProject.Controllers
{
    [Authorize("Admin")]
    public class CategoryController : Controller
    {
        private readonly IDataHelper<Category> dataHelper;
        private int PageItem;
        public CategoryController(IDataHelper<Category> dataHelper) {
            this.dataHelper = dataHelper;
            PageItem = 10;
        }
        // GET: CategoryController
        public ActionResult Index(int?id)
        {
            if(id == 0 || id ==null) {            
                return View(dataHelper.GetAllData().Take(PageItem));
                  }
            else
            {
                var data =dataHelper.GetAllData().Where(x => x.Id> id).Take(PageItem);
                return View(data);

            }
        }
        // GET: CategoryController
        public ActionResult Search(string SearchItem)
        {
            if(SearchItem == null) {  return View("index",dataHelper.GetAllData()); }
            else
            {
                return View("index", dataHelper.Serch(SearchItem));
            }
        }

        // GET: CategoryController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category collection)
        {
            try
            {
                var result =dataHelper.Add(collection);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    return View(result);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category collection)
        {
            try
            {
                var result = dataHelper.Edit(id,collection);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    return View(result);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category collection)
        {
            try
            {
                var result = dataHelper.Delete(id);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    return View(result);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
