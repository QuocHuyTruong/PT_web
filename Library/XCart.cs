using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;
namespace BookStore
{
    public class XCart
    {
        Model db = new Model();
        public List<CartItem> AddCart(CartItem cart,string productid)
        {
            List<CartItem> listcart;
            if (System.Web.HttpContext.Current.Session["Cart"].Equals(""))
            {
                listcart = new List<CartItem>();
                //kt hàng còn hay kh
                var li = db.Books.Find(productid);
                if (cart.Qty <= li.Quantity)
                {
                    cart.Status = true;
                }
                listcart.Add(cart);
                System.Web.HttpContext.Current.Session["Cart"] = listcart;
            }
            else
            {
                listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["Cart"]; //Ep kieu
                if (listcart.Where(m => m.ProductID == productid).Count() != 0)
                {
                    int vt = 0;
                    foreach (var tnn in listcart)
                    {
                        if (tnn.ProductID == productid)
                        {
                            listcart[vt].Qty += 1;
                            listcart[vt].Amount = listcart[vt].Qty * listcart[vt].Price;
                            //kt hàng còn hay kh
                            var li = db.Books.Find(productid);
                            if (listcart[vt].Qty <= li.Quantity)
                            {
                                listcart[vt].Status = true;
                            }
                            else
                            {
                                listcart[vt].Status = false;
                            }    
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["Cart"] = listcart;
                }
                else
                {
                    //kt hàng còn hay kh
                    var li = db.Books.Find(productid);
                    if (cart.Qty <= li.Quantity)
                    {
                        cart.Status = true;
                    }
                    listcart.Add(cart);
                    System.Web.HttpContext.Current.Session["Cart"] = listcart;
                }

            }
            return listcart;
        }

        public void CartDell(string productid)
        {
            if (!System.Web.HttpContext.Current.Session["Cart"].Equals(""))
            {
                List<CartItem> listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["Cart"];
                int vt = 0;
                foreach (var tnn in listcart)
                {
                    if (tnn.ProductID == productid)
                    {
                        listcart.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                System.Web.HttpContext.Current.Session["Cart"] = listcart;
            }
        }
        public List<CartItem> getCart()
        {
            if (System.Web.HttpContext.Current.Session["Cart"].Equals(""))
            {
                return null;
            }
            return (List<CartItem>)System.Web.HttpContext.Current.Session["Cart"];
        }
        public String getNewID()
        {
            var countOfRows = db.Orders.Count();
            if (countOfRows == 0) return "OD-000001";
            var lastRow = db.Orders.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.OrderID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "OD-" + str.PadLeft(6, '0');
        }
        public String getNewOrderID()
        {
            var countOfRows = db.Reviews.Count();
            if (countOfRows == 0) return "RV-0001";
            var lastRow = db.Reviews.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.ReviewID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "RV-" + str.PadLeft(4, '0');
        }
        public String getNewUserID()
        {
            var countOfRows = db.Users.Count();
            if (countOfRows == 0) return "US-001";
            var lastRow = db.Users.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.UserID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "US-" + str.PadLeft(3, '0');
        }
        public String getNewCusID()
        {
            var countOfRows = db.Customers.Count();
            if (countOfRows == 0) return "CS-001";
            var lastRow = db.Customers.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.CustomerID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "CS-" + str.PadLeft(3, '0');
        }

        public String getNewBookID()
        {
            var countOfRows = db.Books.Count();
            if (countOfRows == 0) return "BK-00001";
            var lastRow = db.Books.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.BookID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "BK-" + str.PadLeft(5, '0');
        }
        public String getNewCateID()
        {
            var countOfRows = db.Categories.Count();
            if (countOfRows == 0) return "CT-001";
            var lastRow = db.Categories.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.CategoryID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "CT-" + str.PadLeft(3, '0');
        }
    }
}