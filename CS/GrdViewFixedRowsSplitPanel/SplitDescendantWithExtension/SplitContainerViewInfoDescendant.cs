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
using DevExpress.XtraEditors.Drawing;
using System.Drawing;

namespace GridViewFixedRowsSplitPanel {
    public class SplitContainerViewInfoDescendant : SplitContainerViewInfo {
        public SplitContainerViewInfoDescendant(DevExpress.XtraEditors.SplitContainerControl container)
            : base(container) {

        }
        protected override void UpdatePanelBounds() {
            base.UpdatePanelBounds();
            Rectangle rect = Panel2Info.Bounds; 
            rect.Height += Splitter.Bounds.Height;
            rect.Y -= Splitter.Bounds.Height;
            Panel2Info.Bounds = rect;
            rect = Splitter.Bounds;
            rect.Height = 0;
            Splitter.Bounds = rect;
        }
    }
}
