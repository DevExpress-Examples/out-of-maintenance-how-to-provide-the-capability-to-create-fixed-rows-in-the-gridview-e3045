using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;

namespace GVFixedRows
{
    class GridControlFixedRows : GridControl
    {
        protected override void RegisterAvailableViewsCore(DevExpress.XtraGrid.Registrator.InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new GridFixedRowsViewInfoRegistrator());
        }

        protected override DevExpress.XtraGrid.Views.Base.BaseView CreateDefaultView()
            { return CreateView("GridViewFixedRows"); }
    }
}
