

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

        protected override void CalcInfo() {
            base.CalcInfo();
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
