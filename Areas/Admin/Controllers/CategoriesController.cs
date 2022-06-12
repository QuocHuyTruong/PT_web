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
    public class CategoriesController : BaseController
    {
        private Model db = new Model();
        private XCart xCart = new XCart();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.CategoryGroup);
            return View(categories.ToList());
        }
        [HttpPost]
        public ActionResult Index(string id, string ten)
        {
            var categories = db.Categories.Where(m => m.CategoryID.Contains(id) && (m.CategoryName).Contains(ten));
            return View(categories.ToList());
        }
        // GET: Admin/Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(category);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("Error", "Error");
            }
            ViewBag.CateGroupID = new SelectList(db.CategoryGroups, "GroupID", "GroupName", category.CateGroupID);
            return View(category);
        }

        // POST: Admin/Categories1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,CategoryDescription,Url,CateGroupID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateGroupID = new SelectList(db.CategoryGroups, "GroupID", "GroupName", category.CateGroupID);
            return View(category);
        }
        public ActionResult Create()
        {
            ViewBag.CateGroupID = new SelectList(db.CategoryGroups, "GroupID", "GroupName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection field)
        {
            Category category = new Category();
            category.CategoryID = xCart.getNewCateID();
            category.CategoryName = field["tentheloai"];
            category.CategoryDescription = field["mota"];
            category.Url = field["url"];
            category.CateGroupID = field["nhom"];
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
