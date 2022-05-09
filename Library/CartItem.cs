using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore
{
    public class CartItem
    {
        public string ProductID;
        public string Name;
        public string Img;
        public decimal Price;
        public int Qty;
        public decimal Amount;
        public bool Status;

        public CartItem()
        {

        }
        public CartItem(string proid, string name, string img, decimal price, int qty, bool sta)
        {
            this.ProductID = proid;
            this.Name = name;
            this.Img = img;
            this.Price = price;
            this.Qty = qty;
            this.Amount = price * qty;
            this.Status = sta;
        }
    }
}