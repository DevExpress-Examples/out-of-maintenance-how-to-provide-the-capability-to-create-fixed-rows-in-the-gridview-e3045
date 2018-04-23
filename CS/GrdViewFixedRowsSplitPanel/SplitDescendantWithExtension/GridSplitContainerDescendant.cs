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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace GridViewFixedRowsSplitPanel {
    public class GridSplitContainerDescendant : GridSplitContainer {
        protected override DevExpress.XtraEditors.Drawing.SplitContainerViewInfo CreateContainerInfo() {
            return new SplitContainerViewInfoDescendant(this);
        }
        protected override void OnSplitterPositionChanged() {
            base.OnSplitterPositionChanged();
        }
    }
}
