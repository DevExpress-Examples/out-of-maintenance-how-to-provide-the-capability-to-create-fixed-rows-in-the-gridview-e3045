Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Base.Handler
Imports DevExpress.XtraGrid.Views.Base.ViewInfo
Imports DevExpress.XtraGrid.Registrator

Namespace GVFixedRows
	Friend Class GridFixedRowsViewInfoRegistrator
		Inherits GridInfoRegistrator
		Public Overrides ReadOnly Property ViewName() As String
			Get
				Return "GridViewFixedRows"
			End Get
		End Property
		Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
				Return New GridViewFixedRows(TryCast(grid, GridControl))
		End Function
		Public Overrides Function CreateViewInfo(ByVal view As BaseView) As BaseViewInfo
				Return New GridViewInfoFixedRows(TryCast(view, GridViewFixedRows))
		End Function
		Public Overrides Function CreatePainter(ByVal view As BaseView) As BaseViewPainter
				Return New GridFixedRowsPainter(TryCast(view, GridViewFixedRows))
		End Function
	End Class
End Namespace
