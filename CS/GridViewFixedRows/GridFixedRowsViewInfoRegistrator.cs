using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Registrator;

namespace GVFixedRows
{
    class GridFixedRowsViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName { get { return "GridViewFixedRows"; } }
        public override BaseView CreateView(GridControl grid)
            { return new GridViewFixedRows(grid as GridControl); }
        public override BaseViewInfo CreateViewInfo(BaseView view)
            { return new GridViewInfoFixedRows(view as GridViewFixedRows); }
        public override BaseViewPainter CreatePainter(BaseView view)
            { return new GridFixedRowsPainter(view as GridViewFixedRows); }
    }
}
