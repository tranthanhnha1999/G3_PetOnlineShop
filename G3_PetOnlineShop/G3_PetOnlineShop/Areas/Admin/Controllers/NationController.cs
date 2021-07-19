using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;

namespace G3_PetOnlineShop.Areas.Admin.Controllers
{
    public class NationController : Controller
    {
        PetOnlineShopDBEntities1 db = new PetOnlineShopDBEntities1();
        // GET: Admin/Nation
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.Nations.ToList();
            return View(model);
        }
        public ActionResult Details(string id)
        {
            var model = db.Nations.Find(id);
            return View(model);
        }
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            return View();
        }
        [HttpPost,ActionName ("Create")]
        public ActionResult Create(Nation nation)
        {
            if (ModelState.IsValid)
            {
                db.Nations.Add(nation);
                db.SaveChanges(); 
            }
            return RedirectToAction("Index","Nation");
        }
        public ActionResult Delete(String id)
        {
            var model = db.Nations.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConFirmDelete(string id)
        {
            var model = db.Nations.Find(id);
            db.Nations.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Nation");
        }
        public ActionResult Edit(string id)
        {
            var model = db.Nations.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Nation nation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Nation");
            }        
            return View(nation);
        }
    }
}