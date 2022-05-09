using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using System.Data.Entity;
namespace BookStore.Controllers
{
    public class GioihangController : Controller
    {
        private Model db = new Model();
        XCart xcart = new XCart();
        // GET: Gioihang
        public ActionResult Index()
        {
            if(TempData["status"]!=null)
            {
                ViewBag.statusmsg = TempData["status"];
            }
            List<CartItem> listcart = xcart.getCart(); //Ep kieu
            return View("Index",listcart);
        }
        public ActionResult SingleProduct(string productid)
        {
            Book book = db.Books.Find(productid);
            ViewBag.ID = productid;
            return View(book);
        }
        public ActionResult CartAdd(string  productid)
        {
            var sa = db.Books.Where(m => m.BookID == productid).FirstOrDefault();
            CartItem cart = new CartItem(sa.BookID, sa.BookName, sa.Avatar, sa.Price, 1, false);
            //Add vao gio hang
            List<CartItem> listcart = xcart.AddCart(cart,productid);        
            return RedirectToAction("Index", "Gioihang");
        }
        public ActionResult CartDel(string productid)
        {
            xcart.CartDell(productid);
            return RedirectToAction("Index", "Gioihang");
        }
        public ActionResult Payment(int type)
        {
            if(!Session["KhachHang"].Equals(""))
            {
                if (type == 1)
                {
                    return RedirectToAction("Payment1", "Gioihang");
                }
                else
                {
                    return RedirectToAction("Payment2", "Gioihang");
                }

            }
            return RedirectToAction("Login","Khachhang");
        }
        public ActionResult Payment1()
        {
            //kt còn hàng hay kh
            List<CartItem> listcart = xcart.getCart(); //Ep kieu
            int vt = 0;
            foreach (var tnn in listcart)
            {
                if (tnn.Status == false)
                {
                    TempData["status"] = "Có sách không đủ số lượng bán. Vui lòng xóa";
                    return RedirectToAction("Index", "Gioihang");
                }
                vt++;
            }
            //
            var user = Session["KhachHang"].ToString();
            Customer cus = new Customer();
            cus = db.Customers.Where(m => m.UserID == user).ToList().FirstOrDefault();
            ViewBag.cus = cus;
            return View();
        }
        public ActionResult Payment2()
        {
            //kt còn hàng hay kh
            List<CartItem> listcart = xcart.getCart(); //Ep kieu
            int vt = 0;
            foreach (var tnn in listcart)
            {
                if (tnn.Status == false)
                {
                    TempData["status"] = "Có sách không đủ số lượng bán. Vui lòng xóa";
                    return RedirectToAction("Index", "Gioihang");
                }
                vt++;
            }
            //
            var user = Session["KhachHang"].ToString();
            Customer cus = new Customer();
            cus = db.Customers.Where(m => m.UserID == user).ToList().FirstOrDefault();
            ViewBag.cus = cus;
            return View();
        }
        public ActionResult Datmua(FormCollection field, int type)
        {
            decimal total = 0;
            List<CartItem> listcart = xcart.getCart();
            var user = Session["KhachHang"].ToString();
            Customer cus = new Customer();
            cus = db.Customers.Where(m => m.UserID == user).ToList().FirstOrDefault();
            Order order = new Order();
            order.OrderID = xcart.getNewID();
            order.OrderByDate = DateTime.Now;
            order.Status = "Chờ duyệt";
            order.Notes = field["note"];
            order.CustomerID = cus.CustomerID;
            if(type==1)
            {
                order.Address = field["diachi"];
                order.PaymentID = "PM-001";
            }
            else
            {
                order.Address = field["diachi"];
                order.PaymentID = "PM-002";
            }
            foreach(var item in listcart)
            {
                total = total + (item.Amount);
            }
            order.Total = (int)total;
            db.Orders.Add(order);
            db.SaveChanges();
            foreach(CartItem cartItem in listcart)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = order.OrderID;
                orderDetail.BookID = cartItem.ProductID;
                orderDetail.Price = (int)cartItem.Price;
                orderDetail.Quantity = cartItem.Qty;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            TempData["comp"] = "Đặt hàng thành công. Xin cảm ơn";
            return RedirectToAction("Index", "Book");
        }
        public ActionResult Test()
        {
            string ID = "BK-00001";
            var li = db.Books.Find(ID);
            List<CartItem> listcart = xcart.getCart(); //Ep kieu
            int list = listcart.Where(m => m.ProductID == ID).Count();
            if(list>li.Quantity)
            {
                ViewBag.status = "kh co hang";
            }
            else
            {
                ViewBag.status = list;
            }
            return View("Test", listcart);
        }
    }
}