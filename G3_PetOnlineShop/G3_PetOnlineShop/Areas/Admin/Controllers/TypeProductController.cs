﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;

namespace G3_PetOnlineShop.Areas.Admin.Controllers
{
    public class TypeProductController : Controller
    {
        public PetOnlineShopDBEntities1 db = new PetOnlineShopDBEntities1();
        // GET: Admin/TypeProduct
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var models = db.TypeProducts.ToList();
            return View(models);
        }
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            return View();
        }
        [HttpPost]
        public ActionResult Create(TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.TypeProducts.Add(typeProduct);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "TypeProduct");
        }
        public ActionResult Details(string id)
        {
            var model = db.TypeProducts.Find(id);
            return View(model);
        }
        public ActionResult Delete(string id)
        {
            var model = db.TypeProducts.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            var item = db.TypeProducts.Find(id);
            db.TypeProducts.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index", "TypeProduct");
        }
        public ActionResult Edit(string id)
        {
            var model = db.TypeProducts.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult ConfirmEdit(TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
            db.Entry(typeProduct).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "TypeProduct");
            }
            return View(typeProduct);
        }
    }
}