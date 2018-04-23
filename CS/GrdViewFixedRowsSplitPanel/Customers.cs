// Developer Express Code Central Example:
// How to provide the capability to create fixed rows in the GridView
// 
// The current example illustrates how to implement a functionality for creating
// fixed rows, which will be displayed on top of the GridView.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3045

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
