using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;
using PagedList;

namespace G3_PetOnlineShop.Controllers
{
    public class HomeController : Controller
    {
        public PetOnlineShopDBEntities1 db = new PetOnlineShopDBEntities1();
        public IEnumerable<Product> AllListPaging(int page, int pageSize)
        {
            return db.Products.OrderByDescending(x => x.DateOfManufacture).ToPagedList(page, pageSize);
        }
        public IEnumerable<Product> AllListPagingByType(int page, int pageSize,string typeId)
        {
            return db.Products.OrderByDescending(x => x.DateOfManufacture).Where(x=>x.TypeID.Equals(typeId)).ToPagedList(page, pageSize);
        }


        public ActionResult Index(string id,int page = 1 ,int pageSize = 20)
        {
            ViewBag.menu = db.TypeProducts.ToList();

            if (id == null)
            {
                var models = AllListPaging(page, pageSize);
                ViewBag.id = null;
                return View(models);
            }
            else
            {
                var models = AllListPagingByType(page, pageSize, id);
                ViewBag.id = id;
                return View(models);
            }
                
        }

        public ActionResult About()
        {
            ViewBag.menu = db.TypeProducts.ToList();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.menu = db.TypeProducts.ToList();
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(int id)
        {
            ViewBag.menu = db.TypeProducts.ToList();
            var model = db.Products.Find(id);
            return View(model);
        }
    }
}