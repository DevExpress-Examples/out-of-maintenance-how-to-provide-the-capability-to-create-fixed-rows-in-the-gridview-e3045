Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraGrid

Namespace GVFixedRows
	Friend Class GridControlFixedRows
		Inherits GridControl
		Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As DevExpress.XtraGrid.Registrator.InfoCollection)
			MyBase.RegisterAvailableViewsCore(collection)
			collection.Add(New GridFixedRowsViewInfoRegistrator())
		End Sub

		Protected Overrides Function CreateDefaultView() As DevExpress.XtraGrid.Views.Base.BaseView
				Return CreateView("GridViewFixedRows")
		End Function
	End Class
End Namespace
