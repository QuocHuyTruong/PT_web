using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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
            List<Book> books = db.Books.Where(o => o.Quantity <= 5).ToList();
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
        [HttpPost]
        public ActionResult Charts(FormCollection field)
        {
            ViewData["to"] = field["to"];
            ViewData["from"] = field["from"];
            DateTime to = Convert.ToDateTime(field["to"]);
            DateTime from = Convert.ToDateTime(field["from"]);
            DateTime date = DateTime.Now.Date;
            var listbook = db.Orders.Where(o => o.OrderByDate >= to).Where(o => o.OrderByDate <= from).ToList();
            //var listbook = db.Orders.SqlQuery("select * from [Order] where OrderByDate between '%" + field["to"] + "%' and '%" + field["from"] + "%'").ToList();
            return View(listbook.ToList());
        }

        public void exexcel()
        {
            DateTime date = DateTime.Now.Date;
            // Tổng thu nhập
            var listOrderyear = db.Orders.Where(o => o.Status == "Đã duyệt").ToList();
            int moneycountyear = 0;
            foreach (var item in listOrderyear)
            {
                moneycountyear += item.Total;
            }
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

            List<Order> listorder = db.Orders.ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            workSheet.Cells[1, 1].Value = "Thống kê bán hàng của nhà sách Vĩnh Phước";
            workSheet.Cells[1, 1, 1, 5].Merge = true;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;


            workSheet.Cells[3, 1].Value = "Thu nhập trong ngày" + date;
            workSheet.Cells[3, 2].Value = moneycount;

            workSheet.Cells[4, 1].Value = "Tổng số sách bán trong ngày" + date;
            workSheet.Cells[4, 2].Value = sellcount;

            workSheet.Cells[5, 1].Value = "Tổng thu nhập";
            workSheet.Cells[5, 2].Value = moneycountyear;
            workSheet.Row(5).Style.Font.Bold = true;

            //
            workSheet.Row(10).Height = 20;
            workSheet.Row(10).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(10).Style.Font.Bold = true;
            workSheet.Cells[10, 1].Value = "STT";
            workSheet.Cells[10, 2].Value = "Khách hàng";
            workSheet.Cells[10, 3].Value = "Địa chỉ";
            workSheet.Cells[10, 4].Value = "Số điện thoại";
            workSheet.Cells[10, 5].Value = "Tổng tiền";

            int recordIndex = 11;
            foreach (var list in listorder)
            {
                workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                workSheet.Cells[recordIndex, 2].Value = list.Customer.CustomerName;
                workSheet.Cells[recordIndex, 3].Value = list.Address;
                workSheet.Cells[recordIndex, 4].Value = list.Customer.PhoneNumber;
                workSheet.Cells[recordIndex, 5].Value = list.Total;
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            string excelName = "thongkebanhang";
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

        }
    }
}