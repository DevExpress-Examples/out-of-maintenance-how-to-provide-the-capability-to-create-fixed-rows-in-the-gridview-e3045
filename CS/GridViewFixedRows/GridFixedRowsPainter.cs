using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Drawing;

namespace GVFixedRows
{
    class GridFixedRowsPainter : GridPainter
    {
        public GridFixedRowsPainter(GridView view) : base(view) { }

        protected override void DrawRows(GridViewDrawArgs e)
        {
            GridViewFixedRows gv = this.View as GridViewFixedRows;
            GridViewRectsFixedRows rectangles = e.ViewInfo.ViewRects as GridViewRectsFixedRows;
            int[] columnsWidths = new int[e.ViewInfo.ColumnsInfo.Count];

            for (int i = 0; i < e.ViewInfo.ColumnsInfo.Count; i++)
            {
                GridColumnInfoArgs ci = e.ViewInfo.ColumnsInfo[i];
                columnsWidths[i] = ci.Bounds.Width;
            }

            Rectangle r = new Rectangle();
            r.Height = e.ViewInfo.ColumnRowHeight;
            
            int rowCount = 0;
            foreach (DataRow row in gv.FixedRows)
            {
                r.X = rectangles.FixedRows.X - 1;
                for (int i = 0; i < columnsWidths.Length; i++)
                {
                    r.Y = rectangles.FixedRows.Y + e.ViewInfo.ColumnRowHeight * rowCount - 1;
                    r.Width = columnsWidths[i];

                    if (i == 0)
                        DrawIndicatorCell(e, r);
                    else
                    {
                        r.X += columnsWidths[i - 1];
                        DrawDataCell(e, gv, r, row[i-1].ToString());
                    }
                }
                rowCount++;
            }
            base.DrawRows(e);
        }

        void DrawDataCell(GridViewDrawArgs e, GridViewFixedRows gv, Rectangle r, string text)
        {
            e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.LightGray), r);
            e.Graphics.DrawRectangle(e.Cache.GetPen(Color.Gray), r);
            
            Rectangle textR = r;
            textR.Inflate(-4, -2);

            StringFormat sf = gv.Appearance.Row.GetStringFormat();
            if (IsNumeric(text))
                sf.Alignment = StringAlignment.Far;
            else
                sf.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(text, gv.Appearance.Row.Font, e.Cache.GetSolidBrush(Color.Black), textR, sf);
        }

        void DrawIndicatorCell(GridViewDrawArgs e, Rectangle r)
        {
            e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.LightGray), r);
            e.Graphics.DrawRectangle(e.Cache.GetPen(Color.Gray), r);
        }

        bool IsNumeric(string str)
        {
            double num;
            return double.TryParse(str, out num);
        }
    }
}
