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
using DevExpress.XtraGrid;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace GridViewFixedRowsSplitPanel {
    public class FixedRowsExtension {

        Dictionary<int, int> FixedRows = new Dictionary<int, int>();
        List<int> listOfRowsHeights = new List<int>();

        ContextMenu fixedRowAddMenu, fixedRowRemoveMenu;
        
        GridHitInfo hi;
        GridView gridViewPanel1;
        GridView gridViewPanel2;
        int sourceIndex = 0;
        int rowHeight = 0;
        int height = 0;

        GridControl gridControl1 = new GridControl();
        GridSplitContainerDescendant gridSplitContainer1;

        public FixedRowsExtension(GridSplitContainerDescendant split) {
            gridSplitContainer1 = split;
            gridControl1 = gridSplitContainer1.Grid as GridControl;
            gridControl1.ForceInitialize();
            gridViewPanel1 = gridControl1.MainView as GridView;
            gridViewPanel1.OptionsMenu.ShowSplitItem = false;
            gridViewPanel1.OptionsCustomization.AllowGroup = false;
            //add menuItems
            fixedRowAddMenu = new ContextMenu();
            MenuItem add = new MenuItem();
            add.Index = 0;
            add.Text = "Add to fixed rows";
            add.Click += add_Click;
            fixedRowAddMenu.MenuItems.Add(add);
            fixedRowRemoveMenu = new ContextMenu();
            MenuItem remove = new MenuItem();
            remove.Index = 0;
            remove.Text = "Remove from fixed rows";
            remove.Click += remove_Click;
            fixedRowRemoveMenu.MenuItems.Add(remove);

            //show split
            gridSplitContainer1.ShowSplitView();
            gridSplitContainer1.FixedPanel = SplitFixedPanel.Panel1;
            
            //raise events
            gridViewPanel1.CustomRowFilter += gridViewPanel1_CustomRowFilter;
            gridViewPanel1.PopupMenuShowing += gridViewPanel1_PopupMenuShowing;
            gridViewPanel2 = gridSplitContainer1.SplitChildGrid.MainView as GridView;
            gridViewPanel2.CustomRowFilter += gridViewPanel2_CustomRowFilter;
            gridViewPanel2.PopupMenuShowing += gridViewPanel2_PopupMenuShowing;
            gridViewPanel1.CalcRowHeight += gridViewPanel2_CalcRowHeight;
            gridViewPanel2.CalcRowHeight += gridViewPanel2_CalcRowHeight;
            gridViewPanel1.ShowingEditor += gridViewPanel1_ShowingEditor;
            gridViewPanel1.RowCellStyle += gridViewPanel1_RowCellStyle;
            gridViewPanel1.Layout += gridViewPanel1_Layout;
            gridSplitContainer1.Grid.ForceInitialize();
            GridViewInfo viewInfo = gridViewPanel1.GetViewInfo() as GridViewInfo;
            gridSplitContainer1.SplitterPosition = viewInfo.ViewRects.ColumnPanel.Bottom;
        }

        void gridViewPanel1_Layout(object sender, EventArgs e) {
            RefreshData();
        }

        void gridViewPanel1_RowCellStyle(object sender, RowCellStyleEventArgs e) {
            GridViewInfo viewInfo = gridViewPanel1.GetViewInfo() as GridViewInfo;
            e.Appearance.Assign(viewInfo.PaintAppearance.HideSelectionRow);
        }

        private void gridViewPanel2_CalcRowHeight(object sender, RowHeightEventArgs e) {
            if (listOfRowsHeights.Count != gridViewPanel2.RowCount)
                listOfRowsHeights.Add(e.RowHeight);
        }

        void gridViewPanel1_ShowingEditor(object sender, CancelEventArgs e) {
            e.Cancel = true;
        }

        void gridViewPanel1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
            GridView gv = sender as GridView;
            hi = gv.CalcHitInfo(e.Point);
            if (hi.InRow) {
                sourceIndex = gv.GetDataSourceRowIndex(hi.RowHandle);
                fixedRowRemoveMenu.Show(gv.GridControl, e.Point);
            }
        }

        void gridViewPanel2_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
            GridView gv = sender as GridView;
            hi = gv.CalcHitInfo(e.Point);
            if (hi.InRow) {
                sourceIndex = gv.GetDataSourceRowIndex(hi.RowHandle);
                rowHeight = listOfRowsHeights[sourceIndex];
                fixedRowAddMenu.Show(gv.GridControl, e.Point);
            }
        }

        void gridViewPanel1_CustomRowFilter(object sender, RowFilterEventArgs e) {
            if (!FixedRows.ContainsKey(e.ListSourceRow)) {
                e.Visible = false;
                e.Handled = true;
            }
        }

        void gridViewPanel2_CustomRowFilter(object sender, RowFilterEventArgs e) {
            if (FixedRows.ContainsKey(e.ListSourceRow)) {
                e.Visible = false;
                e.Handled = true;
            }
        }

        void add_Click(object sender, EventArgs e) {
            if (!FixedRows.ContainsKey(sourceIndex))
                FixedRows.Add(sourceIndex, rowHeight);
            RefreshData();
        }

        void remove_Click(object sender, EventArgs e) {
            if (FixedRows.ContainsKey(sourceIndex))
                FixedRows.Remove(sourceIndex);
            RefreshData();
        }

        private void RefreshData() {
            gridSplitContainer1.Grid.MainView.RefreshData();
            gridSplitContainer1.SplitChildGrid.MainView.RefreshData();
            ShiftingSplitter();
        }

        private void ShiftingSplitter() {
            GridViewInfo viewInfo = gridViewPanel1.GetViewInfo() as GridViewInfo;
            int splitterPosition = 0;
            height = viewInfo.ViewRects.ColumnPanel.Bottom + 2;
            if (viewInfo.GetGridRowInfo(GridControl.AutoFilterRowHandle) != null)
                height += viewInfo.CalcRowHeight(viewInfo.GInfo.Graphics, GridControl.AutoFilterRowHandle, -1) + 4;
            splitterPosition = height;
            Dictionary<int, int>.ValueCollection values = FixedRows.Values;
            for (int i = 0; i < values.Count; i++)
                height += values.ToArray()[i] + 1;
            splitterPosition = Math.Max(height, splitterPosition);
            if (splitterPosition >= viewInfo.ViewRects.EmptyRows.Bottom)
                splitterPosition -= viewInfo.ViewRects.EmptyRows.Height;
            gridSplitContainer1.SplitterPosition = splitterPosition;
            splitterPosition -= viewInfo.ViewRects.EmptyRows.Bottom - viewInfo.ViewRects.EmptyRows.Top;
            if (viewInfo.ViewRects.EmptyRows.Top != 0)
                gridSplitContainer1.SplitterPosition = splitterPosition;
        }
    }
}