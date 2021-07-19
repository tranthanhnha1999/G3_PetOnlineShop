using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;

namespace G3_PetOnlineShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        PetOnlineShopDBEntities1 db = new PetOnlineShopDBEntities1();
        // GET: Admin/Order
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.Orderproes.ToList();
            return View(model);
        }
        public ActionResult Details(int id)
        {
            ViewBag.od = db.Orderproes.Find(id);
            var models = db.OrderDetails.Where(p => p.OrderID == id);
            return View(models);
        }
    }
}