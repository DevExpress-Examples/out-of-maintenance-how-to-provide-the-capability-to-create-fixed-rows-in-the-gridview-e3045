Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Drawing
Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Drawing

Namespace GVFixedRows
	Friend Class GridFixedRowsPainter
		Inherits GridPainter
		Public Sub New(ByVal view As GridView)
			MyBase.New(view)
		End Sub

		Protected Overrides Sub DrawRows(ByVal e As GridViewDrawArgs)
			Dim gv As GridViewFixedRows = TryCast(Me.View, GridViewFixedRows)
			Dim rectangles As GridViewRectsFixedRows = TryCast(e.ViewInfo.ViewRects, GridViewRectsFixedRows)
			Dim columnsWidths(e.ViewInfo.ColumnsInfo.Count - 1) As Integer

			For i As Integer = 0 To e.ViewInfo.ColumnsInfo.Count - 1
				Dim ci As GridColumnInfoArgs = e.ViewInfo.ColumnsInfo(i)
				columnsWidths(i) = ci.Bounds.Width
			Next i

			Dim r As New Rectangle()
			r.Height = e.ViewInfo.ColumnRowHeight

			Dim rowCount As Integer = 0
			For Each row As DataRow In gv.FixedRows
				r.X = rectangles.FixedRows.X - 1
				For i As Integer = 0 To columnsWidths.Length - 1
					r.Y = rectangles.FixedRows.Y + e.ViewInfo.ColumnRowHeight * rowCount - 1
					r.Width = columnsWidths(i)

					If i = 0 Then
						DrawIndicatorCell(e, r)
					Else
						r.X += columnsWidths(i - 1)
						DrawDataCell(e, gv, r, row(i-1).ToString())
					End If
				Next i
				rowCount += 1
			Next row
			MyBase.DrawRows(e)
		End Sub

		Private Sub DrawDataCell(ByVal e As GridViewDrawArgs, ByVal gv As GridViewFixedRows, ByVal r As Rectangle, ByVal text As String)
			e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.LightGray), r)
			e.Graphics.DrawRectangle(e.Cache.GetPen(Color.Gray), r)

			Dim textR As Rectangle = r
			textR.Inflate(-4, -2)

			Dim sf As StringFormat = gv.Appearance.Row.GetStringFormat()
			If IsNumeric(text) Then
				sf.Alignment = StringAlignment.Far
			Else
				sf.Alignment = StringAlignment.Near
			End If
			e.Graphics.DrawString(text, gv.Appearance.Row.Font, e.Cache.GetSolidBrush(Color.Black), textR, sf)
		End Sub

		Private Sub DrawIndicatorCell(ByVal e As GridViewDrawArgs, ByVal r As Rectangle)
			e.Graphics.FillRectangle(e.Cache.GetSolidBrush(Color.LightGray), r)
			e.Graphics.DrawRectangle(e.Cache.GetPen(Color.Gray), r)
		End Sub

		Private Function IsNumeric(ByVal str As String) As Boolean
			Dim num As Double
			Return Double.TryParse(str, num)
		End Function
	End Class
End Namespace
