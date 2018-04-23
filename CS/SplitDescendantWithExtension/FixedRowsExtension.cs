

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
using DevExpress.XtraGrid.Scrolling;

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
        ScrollBarPresence panel2State = ScrollBarPresence.Unknown;
        GridControl gridControl1 = new GridControl();
        GridSplitContainerDescendant gridSplitContainer1;

        public FixedRowsExtension(GridSplitContainerDescendant split) {
            gridSplitContainer1 = split;
            gridControl1 = gridSplitContainer1.Grid as GridControl; 
            gridControl1.ForceInitialize();
            gridViewPanel1 = gridControl1.MainView as GridView;
            gridViewPanel1.OptionsMenu.ShowSplitItem = false;
            gridViewPanel1.OptionsCustomization.AllowGroup = false;

            var scrollBar = gridControl1.Controls.OfType<Control>().FirstOrDefault(x => x is VCrkScrollBar);
                if (scrollBar != null)
                    gridControl1.Controls.Remove(scrollBar);
            
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
            gridViewPanel1.Layout += gridViewPanel1_Layout;
            gridSplitContainer1.Grid.ForceInitialize();
            gridViewPanel1.FocusedRowChanged += gridViewPanel1_FocusedRowChanged;
            gridSplitContainer1.Panel2.SizeChanged += Panel2_SizeChanged;
            gridViewPanel2.RowCountChanged += gridViewPanel2_RowCountChanged;
            GridViewInfo viewInfo1 = gridViewPanel1.GetViewInfo() as GridViewInfo;
            gridSplitContainer1.SplitterPosition = viewInfo1.ViewRects.ColumnPanel.Bottom;
            GridViewInfo viewInfo2 = gridViewPanel2.GetViewInfo() as GridViewInfo;
            panel2State = viewInfo2.VScrollBarPresence;
        }

        void gridViewPanel2_RowCountChanged(object sender, EventArgs e) {
            ColumnSynk();
        }


        void gridViewPanel1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e) {
            if(e.FocusedRowHandle == (sender as GridView).RowCount - 1 && (gridViewPanel2.GetViewInfo() as GridViewInfo).VScrollBarPresence == ScrollBarPresence.Hidden) {
                (sender as GridView).RowHeight += 1;
                RefreshData();
            }
        }

        private void ColumnSynk ()
        {
            GridViewInfo viewInfo2 = gridViewPanel2.GetViewInfo() as GridViewInfo;
            if(viewInfo2.VScrollBarPresence != panel2State) {
                panel2State = viewInfo2.VScrollBarPresence;
                if(viewInfo2.VScrollBarPresence == ScrollBarPresence.Visible) {
                    gridViewPanel1.VertScrollVisibility = ScrollVisibility.Always;
                    RefreshData();
                } else {
                    if(FixedRows.Count != 0 || gridViewPanel1.VertScrollVisibility != ScrollVisibility.Auto)
                        gridViewPanel1.VertScrollVisibility = ScrollVisibility.Never;
                }
            }
        }

        void Panel2_SizeChanged(object sender, EventArgs e) {
            ColumnSynk();
        }

        void gridViewPanel1_Layout(object sender, EventArgs e) {
            ColumnSynk();
            RefreshData();
        }

        private void gridViewPanel2_CalcRowHeight(object sender, RowHeightEventArgs e) {
            if(listOfRowsHeights.Count != gridViewPanel2.RowCount)
                listOfRowsHeights.Add(e.RowHeight);
        }

        void gridViewPanel1_ShowingEditor(object sender, CancelEventArgs e) {
            e.Cancel = true;
        }

        void gridViewPanel1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
            GridView gv = sender as GridView;
            hi = gv.CalcHitInfo(e.Point);
            if(hi.InRow) {
                sourceIndex = gv.GetDataSourceRowIndex(hi.RowHandle);
                fixedRowRemoveMenu.Show(gv.GridControl, e.Point);
            }
        }

        void gridViewPanel2_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
            GridView gv = sender as GridView;
            hi = gv.CalcHitInfo(e.Point);
            if(hi.InRow) {
                sourceIndex = gv.GetDataSourceRowIndex(hi.RowHandle);
                rowHeight = listOfRowsHeights[sourceIndex];
                fixedRowAddMenu.Show(gv.GridControl, e.Point);
            }
        }

        void gridViewPanel1_CustomRowFilter(object sender, RowFilterEventArgs e) {
            if(!FixedRows.ContainsKey(e.ListSourceRow)) {
                e.Visible = false;
                e.Handled = true;
            }
        }

        void gridViewPanel2_CustomRowFilter(object sender, RowFilterEventArgs e) {
            if(FixedRows.ContainsKey(e.ListSourceRow)) {
                e.Visible = false;
                e.Handled = true;
            }
        }

        void add_Click(object sender, EventArgs e) {
            if(!FixedRows.ContainsKey(sourceIndex))
                FixedRows.Add(sourceIndex, rowHeight);
            if((gridViewPanel2.GetViewInfo() as GridViewInfo).VScrollBarPresence == ScrollBarPresence.Visible) {
                gridViewPanel1.VertScrollVisibility = ScrollVisibility.Always;
            } else {
                gridViewPanel1.VertScrollVisibility = ScrollVisibility.Never;
            }
            RefreshData();
        }

        void remove_Click(object sender, EventArgs e) {
            if(FixedRows.ContainsKey(sourceIndex))
                FixedRows.Remove(sourceIndex);
            if(FixedRows.Count == 0 && (gridViewPanel2.GetViewInfo() as GridViewInfo).VScrollBarPresence != ScrollBarPresence.Visible)
                gridViewPanel1.VertScrollVisibility = ScrollVisibility.Never;
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
            if(viewInfo.GetGridRowInfo(GridControl.AutoFilterRowHandle) != null)
                height += viewInfo.CalcRowHeight(viewInfo.GInfo.Graphics, GridControl.AutoFilterRowHandle, -1, 0) + 4;
            splitterPosition = height;
            Dictionary<int, int>.ValueCollection values = FixedRows.Values;
            for(int i = 0; i < values.Count; i++)
                height += values.ToArray()[i] + 1;
            splitterPosition = Math.Max(height, splitterPosition);
            if(splitterPosition >= viewInfo.ViewRects.EmptyRows.Bottom)
                splitterPosition -= viewInfo.ViewRects.EmptyRows.Height;
            gridSplitContainer1.SplitterPosition = splitterPosition;
            splitterPosition -= viewInfo.ViewRects.EmptyRows.Bottom - viewInfo.ViewRects.EmptyRows.Top;
            if(viewInfo.ViewRects.EmptyRows.Top != 0)
                gridSplitContainer1.SplitterPosition = splitterPosition;
        }
    }
}