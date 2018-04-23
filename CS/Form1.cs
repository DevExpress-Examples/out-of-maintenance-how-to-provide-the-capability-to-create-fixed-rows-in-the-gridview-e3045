

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

        private BindingList<Customers> _Ds;
        BindingList<Customers> listOfCustomers = new BindingList<Customers>();

        FixedRowsExtension Extension;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Extension = new FixedRowsExtension(gridSplitContainer1);

            //fill datasource of gridControl1
            _Ds = FillTables();
            gridSplitContainer1.Grid.DataSource = _Ds;
            gridSplitContainerDescendant1View.OptionsView.ColumnAutoWidth = false;

            RepositoryItemLookUpEdit rilue = new RepositoryItemLookUpEdit();
            rilue.DisplayMember = "Customer";
            rilue.ValueMember = "PurchasePrice";
            rilue.DataSource = _Ds;
            gridSplitContainer1.Grid.RepositoryItems.Add(rilue);
            (gridSplitContainer1.View as GridView).Columns["PurchasePrice"].ColumnEdit = rilue;
        }

        BindingList<Customers> FillTables() {
            listOfCustomers.Clear();
            for (int i = 0; i < 30; i++)
                listOfCustomers.Add(new Customers(string.Format("Customer{0}", i + 1), i * 100));
            return listOfCustomers;
        }
    }
}