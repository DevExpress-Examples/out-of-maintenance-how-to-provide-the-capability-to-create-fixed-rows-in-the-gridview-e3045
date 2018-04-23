
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace GridViewFixedRowsSplitPanel {
    public class GridSplitContainerDescendant : GridSplitContainer
    {
        protected override DevExpress.XtraEditors.Drawing.SplitContainerViewInfo CreateContainerInfo() {
            return new SplitContainerViewInfoDescendant(this);
        }
        protected override void OnSplitterPositionChanged() {
            base.OnSplitterPositionChanged();
        }
    }
}
