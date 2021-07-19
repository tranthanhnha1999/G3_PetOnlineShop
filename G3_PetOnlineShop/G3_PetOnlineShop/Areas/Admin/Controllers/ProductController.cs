using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;

namespace G3_PetOnlineShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        public PetOnlineShopDBEntities1 DB = new PetOnlineShopDBEntities1();
        // GET: Admin/Product
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var models = DB.Products.OrderByDescending(p => p.DateOfManufacture).ToList();
            return View(models);
        }
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            ViewBag.types = new SelectList(DB.TypeProducts.ToList(), "typeID", "typeName");
            ViewBag.nation = new SelectList(DB.Nations.ToList(), "nationID", "nationName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase Images)
        {
            if (Images != null && Images.ContentLength > 0)
            {
                product.Images = Images.FileName;
                string urlImages = Server.MapPath("~/Content/Images/" + product.Images);
                Images.SaveAs(urlImages);
            }
            if (ModelState.IsValid)
            {
                DB.Products.Add(product);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listNation = new SelectList(DB.Nations, "nationID", "nationName", product.nationID);
            ViewBag.listTypes = new SelectList(DB.TypeProducts, "typeID", "typeName", product.TypeID);
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var model = DB.Products.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ComfirmDelete(int id)
        {
            var item = DB.Products.Find(id);
            var orderDetails1 = DB.OrderDetails.ToList();
            int check = 0;
            foreach (OrderDetail i in orderDetails1)
            {
                if (id == i.ProductID)
                {
                    check = check + 1;
                }
            }

            if (check > 0)
            {
                TempData["check"] = "Sản phẩm này đã tôn tại trong chi tiết hóa đơn ,vui lòng xóa tại chi tiết hóa đơn trước khi xóa sản phẩm.";
                return RedirectToAction("Delete", "Product");
                
            }
            else
            {
                DB.Products.Remove(item);
                DB.SaveChanges();
                TempData["check"] = "Bạn đã xóa thành công";
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult Details(int id)
        {
            var model = DB.Products.Find(id);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            ViewBag.types = new SelectList(DB.TypeProducts.ToList(), "typeID", "typeName");
            ViewBag.nation = new SelectList(DB.Nations.ToList(), "nationID", "nationName");
            var model = DB.Products.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Product product,HttpPostedFileBase newImages)
        {
            if(newImages != null && newImages.ContentLength >0)
            {
                product.Images = newImages.FileName;
                string urlImage = Server.MapPath("~/Content/images/"+ product.Images);
                newImages.SaveAs(urlImage);
            }
            if (ModelState.IsValid)
            {
                DB.Entry(product).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            ViewBag.listNation = new SelectList(DB.Nations, "nationID", "nationName", product.nationID);
            ViewBag.listTypes = new SelectList(DB.TypeProducts, "typeID", "typeName", product.TypeID);
            return View(product);
        }
    }
    
}