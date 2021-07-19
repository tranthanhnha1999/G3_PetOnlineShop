using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;

namespace G3_PetOnlineShop.Controllers
{
   
    public class LoginController : Controller
    {
        public PetOnlineShopDBEntities1 db = new PetOnlineShopDBEntities1();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.menu = db.TypeProducts.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index( string username,string password)
        {
            ViewBag.menu = db.TypeProducts.ToList();
            string pass = Encode.EncodeMd5(password);
            var rs = db.Customers.Where(p => p.UserName.Equals(username) && p.PassWord.Equals(pass)).FirstOrDefault();
            if (rs != null)
            {
                Session["Customer"] = rs.CustomerID;
                return RedirectToAction("Index", "Cart");
            }
            else
                return RedirectToAction("Index", "Login"); 
        }
        public ActionResult Register()
        {
            ViewBag.menu = db.TypeProducts.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid){
                customer.PassWord = Encode.EncodeMd5(customer.PassWord);
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }


    }
}