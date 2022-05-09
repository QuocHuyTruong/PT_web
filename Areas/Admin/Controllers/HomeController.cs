using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private Model db = new Model();
        // GET: Admin/Home
        public ActionResult Index()
        {
            DateTime date = DateTime.Now.Date;
            // Tính số tiền và số sách bán trong ngày
            var listOrder = db.Orders.Where(o => o.Status == "Đã duyệt").Where(o => o.OrderByDate == date).ToList();
            int moneycount = 0;
            int sellcount = 0;
            foreach (var item in listOrder)
            {
                moneycount += item.Total;

                foreach (var detail in item.OrderDetails)
                {
                    sellcount += detail.Quantity;
                }
            }
            ViewData["moneycount"] = "" + moneycount;
            ViewData["sellcount"] = sellcount;

            // Tổng thu nhập
            var listOrderyear = db.Orders.Where(o => o.Status == "Đã duyệt").ToList();
            int moneycountyear = 0;
            foreach (var item in listOrderyear)
            {
                moneycountyear += item.Total;
            }
            ViewData["moneycountyear"] = "" + moneycountyear;

            // Tính đơn hàng chưa duyệt
            listOrder = db.Orders.Where(o => o.Status == "Chờ duyệt").ToList();
            ViewData["ordercount"] = listOrder.Count;

            // Tính số review
            var review = db.Reviews.ToList();
            ViewData["reviewcount"] = review.Count;

            //Số sách cần bổ sung
            List<Book> books = db.Books.Where(o=>o.Quantity <= 5).ToList();
            return View(books.ToList());
        }
        public ActionResult Charts()
        {
                DateTime date = DateTime.Now.Date;
            // Tính số tiền và số sách bán trong ngày
            var listOrder = db.Orders.Where(o => o.Status == "Đã duyệt").Where(o => o.OrderByDate == date).ToList();
            int moneycount = 0;
            int sellcount = 0;
            foreach (var item in listOrder)
            {
                moneycount += item.Total;

                foreach (var detail in item.OrderDetails)
                {
                    sellcount += detail.Quantity;
                }
            }
            ViewData["moneycount"] = "" + moneycount;
            ViewData["sellcount"] = sellcount;

            // Tổng thu nhập
            var listOrderyear = db.Orders.Where(o => o.Status == "Đã duyệt").ToList();
            int moneycountyear = 0;
            foreach (var item in listOrderyear)
            {
                moneycountyear += item.Total;
            }
            ViewData["moneycountyear"] = "" + moneycountyear;

            // Tính đơn hàng chưa duyệt
            listOrder = db.Orders.Where(o => o.Status == "Chờ duyệt").ToList();
            ViewData["ordercount"] = listOrder.Count;

            // Tính số review
            var review = db.Reviews.ToList();
            ViewData["reviewcount"] = review.Count;
            // Sách bán chạy nhất
            Book booksell = db.Books.SqlQuery("select * from book order by TotalSell DESC").FirstOrDefault();
            ViewData["booksell"] = booksell.BookName + " đã bán được " + booksell.TotalSell + " quyển";
            //Đơn hàng trong hôm nay
            var listbook = db.Orders.Where(o => o.OrderByDate == date).ToList();
            return View(listbook.ToList());
        }
    }
}