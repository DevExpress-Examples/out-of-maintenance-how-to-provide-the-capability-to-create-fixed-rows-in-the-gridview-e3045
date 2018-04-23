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
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Helpers;

namespace GridViewFixedRowsSplitPanel {
    public partial class Form1 : Form {

        BindingList<Customers> listOfCustomers = new BindingList<Customers>();

        FixedRowsExtension Extension;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Extension = new FixedRowsExtension(gridSplitContainer1);

            //fill datasource of gridControl1
            gridSplitContainer1.Grid.DataSource = FillTables();
            RepositoryItemLookUpEdit rilue = new RepositoryItemLookUpEdit();
            rilue.DisplayMember = "Customer";
            rilue.ValueMember = "PurchasePrice";
            rilue.DataSource = FillTables();
            gridSplitContainer1.Grid.RepositoryItems.Add(rilue);
            (gridSplitContainer1.View as GridView).Columns["PurchasePrice"].ColumnEdit = rilue;
        }

        BindingList<Customers> FillTables() {
            listOfCustomers.Clear();
            for (int i = 0; i < 50; i++)
                listOfCustomers.Add(new Customers(string.Format("Customer{0}", i + 1), i * 100));
            return listOfCustomers;
        }
    }
}