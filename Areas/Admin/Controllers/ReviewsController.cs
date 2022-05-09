using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Areas.Admin.Controllers
{
    public class ReviewsController : BaseController
    {
        private Model db = new Model();
        // GET: Admin/Reviews
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reviews()
        {
            
            return View(db.Reviews.ToList());
        }
        public ActionResult Details(string id)
        {
            Review review = db.Reviews.Find(id);
            return View(review);
        }
    }
}