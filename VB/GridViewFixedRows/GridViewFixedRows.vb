Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Drawing

Namespace GVFixedRows
	Friend Class GridViewFixedRows
		Inherits GridView
		Public Sub New(ByVal grid As DevExpress.XtraGrid.GridControl)
			MyBase.New(grid)
		fixedRows_Renamed = New List(Of DataRow)()
		End Sub

		Public Sub New()
			Me.New(Nothing)
		AddHandler CustomRowFilter, AddressOf GridViewFixedRow_CustomRowFilter
		End Sub

		Private Sub GridViewFixedRow_CustomRowFilter(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowFilterEventArgs)
			Dim dataRow As DataRow = (TryCast(Me.DataSource, DataView)).Table.Rows(e.ListSourceRow)
			If Me.IsFixedRow(dataRow) Then
				e.Visible = False
				e.Handled = True
			End If
		End Sub

		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "GridViewFixedRows"
			End Get
		End Property

		Private fixedRows_Renamed As List(Of DataRow)
		Public Overridable Sub AddToFixedRows(ByVal dataRow As DataRow)
			If (Not fixedRows_Renamed.Contains(dataRow)) Then
				fixedRows_Renamed.Add(dataRow)
				RefreshData()
			End If
		End Sub

		Public Overridable Sub RemoveFromFixedRows(ByVal dataRow As DataRow)
			fixedRows_Renamed.Remove(dataRow)
			RefreshData()
		End Sub

		Public Overridable Function IsFixedRow(ByVal dataRow As DataRow) As Boolean
			Return fixedRows_Renamed.Contains(dataRow)
		End Function

		Public Overridable ReadOnly Property FixedRows() As List(Of DataRow)
			Get
				Return fixedRows_Renamed
			End Get
		End Property

		Public Function InFixedRowsPane(ByVal point As Point) As Boolean
			Dim rects As GridViewRectsFixedRows = TryCast(Me.ViewInfo.ViewRects, GridViewRectsFixedRows)
			If GridDrawing.PtInRect(rects.FixedRows, point) Then
				Return True
			Else
				Return False
			End If
		End Function

		Public Function GetFixedDataRow(ByVal point As Point) As DataRow
			Dim rects As GridViewRectsFixedRows = TryCast(Me.ViewInfo.ViewRects, GridViewRectsFixedRows)
			Dim Y As Integer = rects.FixedRows.Y
			For i As Integer = 0 To Me.FixedRows.Count - 1
				If point.Y >= Y AndAlso point.Y <= Y + Me.ViewInfo.ColumnRowHeight Then
					Return Me.FixedRows(i)
				End If
				Y += Me.ViewInfo.ColumnRowHeight
			Next i
			Return Nothing
		End Function
	End Class
End Namespace
