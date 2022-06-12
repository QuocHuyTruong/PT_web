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
    public class OrderDetailsController : Controller
    {
        private Model db = new Model();

        // GET: Admin/OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Book).Include(o => o.Order);
            return View(orderDetails.ToList());
        }

        // GET: Admin/OrderDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetail = db.OrderDetails.SqlQuery("Select * from OrderDetail where OrderID like '%" + id + "%'");
            if (orderDetail == null)
            {
                return RedirectToAction("Error","Error");
            }
            Order order = db.Orders.Find(id);
            ViewBag.ma = order.OrderID;
            ViewBag.ten = db.Customers.Find(order.CustomerID).CustomerName;
            ViewBag.diachi = order.Address;
            ViewBag.loai = db.Payments.Find(order.PaymentID).PaymentName;
            ViewBag.ngay = order.OrderByDate;
            ViewBag.ghichu = order.Notes;
            ViewBag.tongtien = order.Total;
            return View(orderDetail.ToList());
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName");
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,BookID,Quantity,Price")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", orderDetail.BookID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return RedirectToAction("Error", "Error");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", orderDetail.BookID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address", orderDetail.OrderID);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,BookID,Quantity,Price")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", orderDetail.BookID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
    }
}
