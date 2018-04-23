using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;

namespace GVFixedRows
{
    class GridViewFixedRows : GridView
    {
        public GridViewFixedRows(DevExpress.XtraGrid.GridControl grid) : base(grid) 
        { fixedRows = new List<DataRow>(); }

        public GridViewFixedRows() : this(null)
        { this.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler(GridViewFixedRow_CustomRowFilter); }

        void GridViewFixedRow_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            DataRow dataRow = (this.DataSource as DataView).Table.Rows[e.ListSourceRow];
            if (this.IsFixedRow(dataRow))
            {
                e.Visible = false;
                e.Handled = true;
            }
        }

        protected override string ViewName { get { return "GridViewFixedRows"; } }

        List<DataRow> fixedRows;
        public virtual void AddToFixedRows(DataRow dataRow)
        {
            if (!fixedRows.Contains(dataRow))
            {
                fixedRows.Add(dataRow);
                RefreshData();
            }
        }

        public virtual void RemoveFromFixedRows(DataRow dataRow)
        {
            fixedRows.Remove(dataRow);
            RefreshData();
        }

        public virtual bool IsFixedRow(DataRow dataRow)
        { return fixedRows.Contains(dataRow); }

        public virtual List<DataRow> FixedRows { get { return fixedRows; } }

        public bool InFixedRowsPane(Point point)
        {
            GridViewRectsFixedRows rects = this.ViewInfo.ViewRects as GridViewRectsFixedRows;
            if (GridDrawing.PtInRect(rects.FixedRows, point))
                return true;
            else
                return false;
        }

        public DataRow GetFixedDataRow(Point point)
        {
            GridViewRectsFixedRows rects = this.ViewInfo.ViewRects as GridViewRectsFixedRows;
            int Y = rects.FixedRows.Y;
            for (int i = 0; i < this.FixedRows.Count; i++)
            {
                if (point.Y >= Y && point.Y <= Y + this.ViewInfo.ColumnRowHeight)
                    return this.FixedRows[i];
                Y += this.ViewInfo.ColumnRowHeight;
            }
            return null;
        }
    }
}
