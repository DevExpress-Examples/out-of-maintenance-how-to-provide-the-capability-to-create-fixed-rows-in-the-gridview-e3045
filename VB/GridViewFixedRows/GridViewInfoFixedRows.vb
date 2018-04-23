Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq
Imports System.Text
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid

Namespace GVFixedRows
	Friend Class GridViewInfoFixedRows
		Inherits GridViewInfo
		Public Sub New(ByVal gridView As GridView)
			MyBase.New(gridView)
		End Sub

		Public Overrides Function CreateViewRects() As GridViewRects
			Return New GridViewRectsFixedRows(Me)
		End Function

		' Add FixedRows rectangle to the layout
		Public Overrides Sub CalcRects(ByVal bounds As Rectangle, ByVal partital As Boolean)
			Dim gv As GridViewFixedRows = TryCast(View, GridViewFixedRows)
			Dim r As Rectangle = Rectangle.Empty
			ViewRects.Bounds = bounds
			ViewRects.Scroll = CalcScrollRect()
			ViewRects.Client = CalcClientRect()
			FilterPanel.Bounds = Rectangle.Empty
			If (Not partital) Then
				CalcRectsConstants()
			End If
			If gv.OptionsView.ShowIndicator Then
				ViewRects.IndicatorWidth = Math.Max(View.IndicatorWidth, ViewRects.MinIndicatorWidth)
			End If
			Dim minTop As Integer = ViewRects.Client.Top
			Dim maxBottom As Integer = ViewRects.Client.Bottom
			If gv.OptionsView.ShowViewCaption Then
				r = ViewRects.Client
				r.Y = minTop
				r.Height = CalcViewCaptionHeight(ViewRects.Client)
				ViewRects.ViewCaption = r
				minTop = ViewRects.ViewCaption.Bottom
			End If
			minTop = UpdateFindControlVisibility(New Rectangle(ViewRects.Client.X, minTop, ViewRects.Client.Width, maxBottom - minTop)).Y
			If gv.OptionsView.ShowGroupPanel Then
				r = ViewRects.Client
				r.Y = minTop
				r.Height = CalcGroupPanelHeight()
				ViewRects.GroupPanel = r
				minTop = ViewRects.GroupPanel.Bottom
			End If
			minTop = CalcRectsColumnPanel(minTop)
			If gv.IsShowFilterPanel Then
				r = ViewRects.Client
				Dim fPanel As Integer = GetFilterPanelHeight()
				r.Y = maxBottom - fPanel
				r.Height = fPanel
				FilterPanel.Bounds = r
				maxBottom = r.Top
			End If
			If gv.FixedRows.Count > 0 Then
				r = ViewRects.Client
				r.Y = minTop
				r.Height = ColumnRowHeight * gv.FixedRows.Count
				Dim vr As GridViewRectsFixedRows = TryCast(ViewRects, GridViewRectsFixedRows)
				vr.FixedRows = r
				minTop = vr.FixedRows.Bottom
			End If
			r = ViewRects.Client
			r.Y = minTop
			r.Height = maxBottom - minTop
			ViewRects.Rows = r
		End Sub
	End Class

	Friend Class GridViewRectsFixedRows
		Inherits GridViewRects
		Private fixedRows_Renamed As Rectangle

		Public Sub New(ByVal viewInfo As GridViewInfo)
			MyBase.New(viewInfo)
		End Sub

		Public Overrides Sub Clear()
			MyBase.Clear()
			fixedRows_Renamed = Rectangle.Empty
		End Sub

		Public Property FixedRows() As Rectangle
			Set(ByVal value As Rectangle)
				fixedRows_Renamed = value
			End Set
			Get
				Return fixedRows_Renamed
			End Get
		End Property

		Public Sub AssignTo(ByVal vr As GridViewRectsFixedRows)
			MyBase.AssignTo(vr)
			vr.FixedRows = Me.FixedRows
		End Sub
	End Class
End Namespace
