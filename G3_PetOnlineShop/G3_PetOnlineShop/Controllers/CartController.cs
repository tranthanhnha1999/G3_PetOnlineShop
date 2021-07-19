using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using G3_PetOnlineShop.Models;

namespace G3_PetOnlineShop.Controllers
{
    public class CartController : Controller
    {
        public PetOnlineShopDBEntities1 db = new PetOnlineShopDBEntities1();
        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.menu = db.TypeProducts.ToList();
            List<CartItem> list = (List<CartItem>)Session["cartSession"];
            return View(list);
        }
        public ActionResult AddItem(int id)
        {
            ViewBag.menu = db.TypeProducts.ToList();
            var cart = Session["cartSession"];
            List<CartItem> list = new List<CartItem>();
            if(cart == null)
            {
                Product product = db.Products.Where(x => x.ProductID == id).SingleOrDefault();
                CartItem item = new CartItem();
                item.product = product;
                item.quantity = 1;
                list.Add(item);
                Session["cartSession"] = list;
            }
            else
            {
                list = (List<CartItem>)Session["cartSession"];
                if(list.Exists(x=> x.product.ProductID == id))
                {
                    foreach(var a in list)
                    {
                        if(a.product.ProductID == id)
                        {
                            a.quantity = a.quantity + 1;
                        }
                        Session["cartSession"] = list;
                    }

                }
                else
                {
                    Product product = db.Products.Where(x => x.ProductID == id).SingleOrDefault();
                    CartItem item = new CartItem();
                    item.product = product;
                    item.quantity = 1;
                    list.Add(item);
                    Session["cartSession"] = list;
                }
            }
            return RedirectToAction("index", "Cart");
        }
        public ActionResult UpdateItem(int id , int quantity)
        {
            List<CartItem> list = (List<CartItem>)Session["cartSession"];
            if(quantity != 0)
            {
                list.Where(p => p.product.ProductID.Equals(id)).FirstOrDefault().quantity = quantity;
            }
            return RedirectToAction("index", "Cart");
        }
        public ActionResult Delete(int id)
        {
            List<CartItem> list = (List<CartItem>)Session["cartSession"];
            CartItem cart = list.Where(p => p.product.ProductID.Equals(id)).FirstOrDefault();
            list.Remove(cart);
            Session["cartSession"]= list;
            return RedirectToAction("index", "Cart");

        }
        public ActionResult Order()
        {
            if (Session["Customer"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Orderpro od = new Orderpro();
                od.OrderID = db.Orderproes.OrderByDescending(p => -p.OrderID).First().OrderID + 1;
                od.OrderDate = DateTime.Now;
                od.CustomerID = (int)Session["customer"];
                od.Status = 0;
                db.Orderproes.Add(od);
                db.SaveChanges();
                List<CartItem> list = (List<CartItem>)Session["cartSession"];
                string message = "";
                float sum = 0;
                foreach(CartItem item in list)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = od.OrderID;
                    orderDetail.ProductID = item.product.ProductID;
                    orderDetail.Quantity = item.quantity;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                    message += "<br/> ProductID: " + item.product.ProductID;
                    message += "<br/> ProductName: " + item.product.ProductName;
                    message += "<br/> Quantity: " + item.quantity;
                    message += "<br/> Price: " + item.product.Price;
                    message += "<br/> Quan*Price: " + string.Format("{0:0,0 VNĐ}", item.quantity * item.product.Price );
                    sum += (float)(item.quantity * item.product.Price);

                }
                message += "<br> Total Money:"  + string.Format("{0:0,0 VNĐ}", sum);
                Customer ct = db.Customers.Find(od.CustomerID);
                SendMail(ct.Email, "New order From Pet Shop wwwPetshop.com", message);
                Session["cartSession"] = null;
                return RedirectToAction("Index", "Home");
            }
        }
        public void SendMail(string address, string subject , string message)
        {
            string email = "nhacuteeee@gmail.com";
            string password = "nha151199";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpclient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpclient.EnableSsl = true;
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Credentials = loginInfo;
            smtpclient.Send(msg);
        }
    }
}