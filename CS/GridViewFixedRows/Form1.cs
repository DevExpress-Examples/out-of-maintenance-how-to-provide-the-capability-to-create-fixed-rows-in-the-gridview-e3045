using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace GVFixedRows
{
    public partial class Form1 : Form
    {
        ContextMenu fixedRowAddMenu, fixedRowRemoveMenu;
        DataRow dataRow;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridControlFixedRows1.DataSource = FillTable();

            fixedRowAddMenu = new ContextMenu();
            MenuItem add = new MenuItem();
            add.Index = 0;
            add.Text = "Add to fixed rows";
            add.Click +=new EventHandler(add_Click);
            fixedRowAddMenu.MenuItems.Add(add);

            fixedRowRemoveMenu = new ContextMenu();
            MenuItem remove = new MenuItem();
            remove.Index = 0;
            remove.Text = "Remove from fixed rows";
            remove.Click += new EventHandler(remove_Click);
            fixedRowRemoveMenu.MenuItems.Add(remove);
            gridViewFixedRows1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(gridViewFixedRows1_PopupMenuShowing);
        }

        void gridViewFixedRows1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridViewFixedRows gv = sender as GridViewFixedRows;
            GridHitInfo hi = gv.CalcHitInfo(e.Point);
            if (hi.InRow)
            {
                dataRow = gv.GetDataRow(hi.RowHandle);
                fixedRowAddMenu.Show(gv.GridControl, e.Point);
            }
            else if (gv.InFixedRowsPane(e.Point))
            {
                dataRow = gv.GetFixedDataRow(e.Point);
                fixedRowRemoveMenu.Show(gv.GridControl, e.Point);
            }
        }

        void add_Click(object sender, EventArgs e)
        { gridViewFixedRows1.AddToFixedRows(dataRow); }

        void remove_Click(object sender, EventArgs e)
        { gridViewFixedRows1.RemoveFromFixedRows(dataRow); }

        DataTable FillTable()
        {
            DataTable _customersTable = new DataTable();
            DataColumn col;
            DataRow row;

            _customersTable.TableName = "Customers";

            col = new DataColumn();
            col.ColumnName = "Customer";
            col.DataType = System.Type.GetType("System.String");
            _customersTable.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "Purchase Price";
            col.DataType = System.Type.GetType("System.Double");
            _customersTable.Columns.Add(col);

            row = _customersTable.NewRow();
            row["Customer"] = "Jack Smith";
            row["Purchase Price"] = 120;
            _customersTable.Rows.Add(row);

            row = _customersTable.NewRow();
            row["Customer"] = "John Doe";
            row["Purchase Price"] = 350;
            _customersTable.Rows.Add(row);

            row = _customersTable.NewRow();
            row["Customer"] = "Jane Doe";
            row["Purchase Price"] = 71;
            _customersTable.Rows.Add(row);

            row = _customersTable.NewRow();
            row["Customer"] = "Sam Piter";
            row["Purchase Price"] = 43;
            _customersTable.Rows.Add(row);

            row = _customersTable.NewRow();
            row["Customer"] = "Dolores Patrick";
            row["Purchase Price"] = 311;
            _customersTable.Rows.Add(row);

            row = _customersTable.NewRow();
            row["Customer"] = "Mike Green";
            row["Purchase Price"] = 249;
            _customersTable.Rows.Add(row);

            row = _customersTable.NewRow();
            row["Customer"] = "Dan West";
            row["Purchase Price"] = 126;
            _customersTable.Rows.Add(row);

            return _customersTable;
        }
    }
}
