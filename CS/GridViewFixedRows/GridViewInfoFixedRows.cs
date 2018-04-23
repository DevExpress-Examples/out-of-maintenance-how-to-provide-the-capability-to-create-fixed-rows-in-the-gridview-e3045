using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace GVFixedRows
{
    class GridViewInfoFixedRows : GridViewInfo
    {
        public GridViewInfoFixedRows(GridView gridView) : base(gridView) { }

        public override GridViewRects CreateViewRects()
        { return new GridViewRectsFixedRows(this); }

        // Add FixedRows rectangle to the layout
        public override void CalcRects(Rectangle bounds, bool partital)
        {
            GridViewFixedRows gv = View as GridViewFixedRows;
            Rectangle r = Rectangle.Empty;
            ViewRects.Bounds = bounds;
            ViewRects.Scroll = CalcScrollRect();
            ViewRects.Client = CalcClientRect();
            FilterPanel.Bounds = Rectangle.Empty;
            if (!partital)
            {
                CalcRectsConstants();
            }
            if (gv.OptionsView.ShowIndicator)
            {
                ViewRects.IndicatorWidth = Math.Max(View.IndicatorWidth, ViewRects.MinIndicatorWidth);
            }
            int minTop = ViewRects.Client.Top;
            int maxBottom = ViewRects.Client.Bottom;
            if (gv.OptionsView.ShowViewCaption)
            {
                r = ViewRects.Client;
                r.Y = minTop;
                r.Height = CalcViewCaptionHeight(ViewRects.Client);
                ViewRects.ViewCaption = r;
                minTop = ViewRects.ViewCaption.Bottom;
            }
            minTop = UpdateFindControlVisibility(new Rectangle(ViewRects.Client.X, minTop, ViewRects.Client.Width, maxBottom - minTop), false).Y;
            
            if (gv.OptionsView.ShowGroupPanel)
            {
                r = ViewRects.Client;
                r.Y = minTop;
                r.Height = CalcGroupPanelHeight();
                ViewRects.GroupPanel = r;
                minTop = ViewRects.GroupPanel.Bottom;
            }
            minTop = CalcRectsColumnPanel(minTop);
            if (gv.IsShowFilterPanel)
            {
                r = ViewRects.Client;
                int fPanel = GetFilterPanelHeight();
                r.Y = maxBottom - fPanel;
                r.Height = fPanel;
                FilterPanel.Bounds = r;
                maxBottom = r.Top;
            }
            if (gv.FixedRows.Count > 0)
            {
                r = ViewRects.Client;
                r.Y = minTop;
                r.Height = ColumnRowHeight * gv.FixedRows.Count;
                GridViewRectsFixedRows vr = ViewRects as GridViewRectsFixedRows;
                vr.FixedRows = r;
                minTop = vr.FixedRows.Bottom;
            }
            r = ViewRects.Client;
            r.Y = minTop;
            r.Height = maxBottom - minTop;
            ViewRects.Rows = r;
        }
    }                                      

    class GridViewRectsFixedRows : GridViewRects
    {
        Rectangle fixedRows;

        public GridViewRectsFixedRows(GridViewInfo viewInfo) : base(viewInfo) { }

        public override void Clear()
        {
            base.Clear();
            fixedRows = Rectangle.Empty;
        }

        public Rectangle FixedRows { set { fixedRows = value; } get { return fixedRows; } }

        public void AssignTo(GridViewRectsFixedRows vr)
        {
            base.AssignTo(vr);
            vr.FixedRows = this.FixedRows;
        }
    }
}
