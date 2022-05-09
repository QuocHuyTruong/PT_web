using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        Model db = new Model();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            if(!Session["AdminUser"].Equals(""))
            {
                return RedirectToAction("Index", "Home");
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
                if (s.Password.Equals(password) && (s.RoleID.Equals("RL-001")||s.RoleID.Equals("RL-002")))
                {
                    Session["AdminUser"] = s.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    str = "Mật khẩu không đúng";
                }
            }
            ViewBag.Error = "<span class='text-danger'>"+str+"</span>";
            return View();
        }
        public ActionResult Logout()
        {
            Session["AdminUser"] = "";
            return RedirectToAction("Login", "Auth");
        }
    }
}