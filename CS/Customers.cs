

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridViewFixedRowsSplitPanel {
    class Customers {
        public Customers(string customer, double price) {
            Customer = customer;
            PurchasePrice = price;
        }
        // Fields...
        private int _Price;
        private double _PurchasePrice;
        private string _Customer;

        public string Customer {
            get { return _Customer; }
            set { _Customer = value; }
        }

        public double PurchasePrice {
            get { return _PurchasePrice; }
            set { _PurchasePrice = value; }
        }

        public int Price {
            get { return _Price; }
            set { _Price = value; }
        }
    }
}
