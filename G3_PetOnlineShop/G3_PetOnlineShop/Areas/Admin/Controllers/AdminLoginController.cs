﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;
using G3_PetOnlineShop.Controllers;
namespace G3_PetOnlineShop.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        public PetOnlineShopDBEntities1 db = new PetOnlineShopDBEntities1();
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username,string password)
        {
            string pass = Encode.EncodeMd5(password);
            var result = db.Staffs.Where(s => s.username.Equals(username) && s.password.Equals(password)).FirstOrDefault();
            if(result != null)
            {
                Session["admin"] = result.staffid;
                return RedirectToAction("Index", "AdminHome");
            }
            else
            {
                return View();
            }
        }
    }
}