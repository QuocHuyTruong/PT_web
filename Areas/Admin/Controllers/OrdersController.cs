using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private Model db = new Model();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            if(TempData["status"] != null)
            {
                ViewBag.msg = TempData["status"];
            }
            if (TempData["error"] != null)
            {
                ViewBag.msg = TempData["error"];
            }
            var orders = db.Orders.Where(o => o.Status.Equals("Chờ duyệt"));
            return View(orders.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(order);
        }
       

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Duyet(string id)
        {
            Order order = db.Orders.Find(id);
            bool flag = true;

            // Kiểm tra có đủ hàng hay không
            foreach (OrderDetail detail in order.OrderDetails)
            {
                if (detail.Quantity > detail.Book.Quantity)
                {
                    flag = false;
                    break;
                }
            }

            //Order order = db.Orders.Find(id);
            if (flag)
            {
                order.Status = "Đã duyệt";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                foreach (OrderDetail detail in order.OrderDetails)
                {
                    Book book = db.Books.Find(detail.Book.BookID);
                    book.Quantity -= detail.Quantity;
                    book.TotalSell += detail.Quantity;
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }
                TempData["status"] = "Duyệt đơn thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Không thể duyệt đơn do thiếu hàng";
                return RedirectToAction("Index");
            }
        }
    }
}
