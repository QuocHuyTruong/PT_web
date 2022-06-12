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
    public class CustomersController : Controller
    {
        private Model db = new Model();

        // GET: Admin/Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.User);
            return View(customers.ToList());
        }
        [HttpPost]
        public ActionResult Index(string ma, string ten)
        {
            var cus = db.Customers.Where(m => m.CustomerID.Equals(ma) || (ma.Equals("") && (m.CustomerName).Contains(ten)));

            return View(cus.ToList());
        }

        // GET: Admin/Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Error", "Error");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", customer.UserID);
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerAddress,CustomerName,Gender,Birth,PhoneNumber,Email,UserID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", customer.UserID);
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(customer);
        }

        // POST: Admin/Customers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
