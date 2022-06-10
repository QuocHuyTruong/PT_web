using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using PagedList;
namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private Model db = new Model();

        // GET: Book
        public ActionResult Index(int? page)
        {
            if (TempData["comp"] != null)
            {
                ViewBag.msg = TempData["comp"];
            }
            List<Book> books = db.Books.ToList();
            if (page == null)
            {
                page = 1;
            }
            //var books = db.Books.Include(b => b.Author).Include(b => b.Producer).OrderBy(b => b.BookID);
            int pagesize = 12;
            int paganumber = (page ?? 1);
            //return View(books.ToList());
            return View(books.ToPagedList(paganumber, pagesize));
        }

        // GET: Book/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName");
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,BookName,Price,DiscountPercent,Quantity,TotalSell,Avatar,CreateByDate,Url,Publisher,PublicByDate,BookCover,Pages,BookDescription,AuthorID,ProducerID")] Book books)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", books.AuthorID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", books.ProducerID);
            return View(books);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", book.ProducerID);
            return View(book);
        }

        // POST: Book/Edit/5
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

        // GET: Book/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
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
        public JsonResult ListName(string q)
        {
            var data = db.Books.Where(x => x.BookName.Contains(q)).Select(x => x.BookName).ToList();
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(int? page, string search, string ten, string hot,string gia)
        {
            List<Book> books = db.Books.ToList();
            if (ten != null)
            {
                books = db.Books.Where(m => m.Author.AuthorName.Contains(ten)).ToList();
            }
            if(search != null)
            {
                books = db.Books.Where(m => m.BookName.Contains(search)).ToList();
            }
            if(hot == "new")
            {
                books = db.Books.SqlQuery("select * from Book order by CreateByDate DESC").ToList();
            }
            if (hot == "bestsell")
            {
                books = db.Books.SqlQuery("select * from Book order by TotalSell DESC").ToList();
            }
            if (gia != null)
            {
                if(gia == "1")
                {
                    books = db.Books.SqlQuery("select * from Book where Price <= 10000").ToList();
                }
                if (gia == "2")
                {
                    books = db.Books.SqlQuery("select * from Book where Price >= 10000 and Price <= 30000").ToList();
                }
                if (gia == "3")
                {
                    books = db.Books.SqlQuery("select * from Book where Price >= 30000 and Price <= 50000").ToList();
                }
                if (gia == "4")
                {
                    books = db.Books.SqlQuery("select * from Book where Price >= 50000 and Price <= 80000").ToList();
                }
                if (gia == "5")
                {
                    books = db.Books.SqlQuery("select * from Book where Price >= 80000 and Price <= 100000").ToList();
                }
                if (gia == "6")
                {
                    books = db.Books.SqlQuery("select * from Book where Price >= 100000").ToList();
                }

            }

            if (page == null)
            {
                page = 1;
            }
            //var books = db.Books.Include(b => b.Author).Include(b => b.Producer).OrderBy(b => b.BookID);
            int pagesize = 12;
            int paganumber = (page ?? 1);
            //return View(books.ToList());
            return View(books.ToPagedList(paganumber, pagesize));
        }
    }
}
