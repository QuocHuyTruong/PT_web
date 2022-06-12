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
    public class BooksController : BaseController
    {
        private Model db = new Model();
        XCart xcart = new XCart();
        // GET: Admin/Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.Producer);
            return View(books.ToList());
        }
        [HttpPost]
        public ActionResult Index(string ma, string ten)
        {
            var books = db.Books.Where(m => m.BookID.Equals(ma) || (ma.Equals("") && (m.BookName).Contains(ten)));

            return View(books.ToList());
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(book);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            ViewBag.listAuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName");
            ViewBag.listProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection field, HttpPostedFileBase avatar)
        {
            Book book = new Book();
            book.BookID = xcart.getNewBookID();
            book.BookName = field["tensach"];
            book.Price = int.Parse(field["gia"]);
            book.DiscountPercent = int.Parse(field["giamgia"]);
            book.Quantity = int.Parse(field["soluong"]);
            book.TotalSell = 0;
            //
            string postedFileName = System.IO.Path.GetFileName(avatar.FileName);
            book.Avatar = postedFileName;
            book.CreateByDate = DateTime.Now;
            book.Url = field["url"];
            book.Publisher = field["nhaxuatban"];
            book.PublicByDate = DateTime.Parse(field["namxuatban"]);
            book.BookCover = field["loaibia"];
            book.Pages = int.Parse(field["sotrang"]);
            book.BookDescription = field["description"];
            book.AuthorID = field["author"];
            book.ProducerID = "PD-001";
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Error", "Error");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", book.ProducerID);
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,BookName,Price,DiscountPercent,Quantity,TotalSell,Avatar,CreateByDate,Url,Publisher,PublicByDate,BookCover,Pages,BookDescription,AuthorID,ProducerID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", book.ProducerID);
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
