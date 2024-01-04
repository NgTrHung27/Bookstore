using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BanSach;
using BanSach.Models;

namespace BanSach.Controllers
{
    public class LoginUserController : Controller
    {
        BanSachEntities database = new BanSachEntities();
        // GET: LoginUser
        // Phương thức tạo view cho Login
        public ActionResult Index()
        {
            return View();
        }
        // Xử lý tìm kiếm ID, password trong AdminUser và thông báo
        [HttpPost]
        public ActionResult LoginAcount(AdminUser _user)
        {
            Session["NameUser"] = null;
            var check = database.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai Info";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = _user.NameUser;
                Session["PasswodUser"] = _user.PasswordUser;
                return RedirectToAction("ProductList", "Products");
            }
        }


        //Register
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
              var check_ID = database.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.AdminUsers.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "ID này đã có rồi !!!";
                    return View();
                }
            }
            return View();        
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("ProductList", "Products");
        }

    }
}