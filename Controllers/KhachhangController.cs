using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class KhachhangController : Controller
    {
        Model db = new Model();
        XCart xcart = new XCart();
        // GET: Khachhang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            if (!Session["KhachHang"].Equals(""))
            {
                var user = Session["KhachHang"].ToString();
                Customer cus = new Customer();
                cus = db.Customers.Where(m => m.UserID == user).ToList().FirstOrDefault();
                ViewBag.cus = cus;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Khachhang");
            }
        }
        [HttpPost]
        public ActionResult Contact(FormCollection field)
        {
            var user = Session["KhachHang"].ToString();
            Customer cus = new Customer();
            cus = db.Customers.Where(m => m.UserID == user).ToList().FirstOrDefault();
            Review review = new Review();
            review.ReviewID = xcart.getNewOrderID();
            review.ReviewByDate = DateTime.Now;
            review.Content = field["mess"];
            review.BookID = "BK-00001";
            review.CustomerID = cus.CustomerID;
            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Index", "Book");
        }
        public ActionResult Login()
        {
            if(TempData["status"] != null)
            {
                ViewBag.msg = TempData["status"];
            }
            if (!Session["KhachHang"].Equals(""))
            {
                return RedirectToAction("Pay", "Gioihang");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection filed)
        {
            string str;
            string username = filed["username"];
            string password = filed["password"];
            str = username + password;
            ViewBag.Error = str;
            var s = db.Users.Where(m => m.UserName == username).FirstOrDefault();
            if (s == null)
            {
                //ten dang nhap kh ton tai
                str = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (s.Password.Equals(password) && s.RoleID.Equals("RL-003"))
                {
                    Session["KhachHang"] = s.UserID;
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    str = "Mật khẩu không đúng";
                }
            }
            ViewBag.Error = "<span class='text-danger'>" + str + "</span>";
            return View();
        }
        public ActionResult Signup(FormCollection filed)
        {
            User user = new User();
            user.UserID = xcart.getNewUserID();
            user.UserName = filed["username"];
            user.Password = filed["password"];
            user.CreatedByDate = DateTime.Now;
            user.RoleID = "RL-003";
            db.Users.Add(user);
            db.SaveChanges();
            //
            Customer cus = new Customer();
            cus.CustomerID = xcart.getNewCusID();
            cus.CustomerAddress = filed["address"];
            cus.CustomerName = filed["fullname"];
            if(filed["gender"].Equals("nam"))
            {
                cus.Gender = true;
            }
            else
            {
                cus.Gender = false;
            }
            cus.Birth = DateTime.Parse(filed["birth"]);
            cus.PhoneNumber = filed["phone"];
            cus.Email = filed["email"];
            cus.UserID = user.UserID;
            db.Customers.Add(cus);
            db.SaveChanges();

            TempData["status"] = "Tạo tài khoản thành công";
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session["KhachHang"] = "";
            return RedirectToAction("Index", "Book");
        }
    }
}