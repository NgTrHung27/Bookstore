using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanSach.Models;

namespace BanSach.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        BanSachEntities database = new BanSachEntities();
        public ActionResult Index(string _name)
        {
            if(_name==null)
                return View(database.Categories.ToList());
            else
            return View(database.Categories.Where(s => s.NameCate.Contains(_name)).ToList());
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                database.Categories.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }

        //Details
        public ActionResult Details(int id)
        {
            return View(database.Categories.Where(c => c.Id == id).FirstOrDefault());
        } 

        //Edit
        public ActionResult Edit(int id)
        {
            return View(database.Categories.Where(c => c.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            try 
            {
                database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi");
            }
        }
        
        //Delete
        public ActionResult Delete(int id)
        {
            return View(database.Categories.Where(c => c.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = database.Categories.Where(c => c.Id == id).FirstOrDefault();
                database.Categories.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
        // Action PartialViewResult
        [ChildActionOnly]
        public PartialViewResult CategoryPartial()
        {
            var cateList = database.Categories.ToList();
            return PartialView(cateList);
        }

    }
}