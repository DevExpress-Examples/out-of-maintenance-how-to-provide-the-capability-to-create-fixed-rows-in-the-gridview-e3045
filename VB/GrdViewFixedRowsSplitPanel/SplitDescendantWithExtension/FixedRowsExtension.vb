' Developer Express Code Central Example:
' How to provide the capability to create fixed rows in the GridView
' 
' The current example illustrates how to implement a functionality for creating
' fixed rows, which will be displayed on top of the GridView.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3045


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraGrid
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base

Namespace GridViewFixedRowsSplitPanel
	Public Class FixedRowsExtension

		Private FixedRows As New Dictionary(Of Integer, Integer)()
		Private listOfRowsHeights As New List(Of Integer)()

		Private fixedRowAddMenu, fixedRowRemoveMenu As ContextMenu

		Private hi As GridHitInfo
		Private gridViewPanel1 As GridView
		Private gridViewPanel2 As GridView
		Private sourceIndex As Integer = 0
		Private rowHeight As Integer = 0
		Private height As Integer = 0

		Private gridControl1 As New GridControl()
		Private gridSplitContainer1 As GridSplitContainerDescendant

		Public Sub New(ByVal split As GridSplitContainerDescendant)
			gridSplitContainer1 = split
			gridControl1 = TryCast(gridSplitContainer1.Grid, GridControl)
			gridControl1.ForceInitialize()
			gridViewPanel1 = TryCast(gridControl1.MainView, GridView)
			gridViewPanel1.OptionsMenu.ShowSplitItem = False
			gridViewPanel1.OptionsCustomization.AllowGroup = False
			'add menuItems
			fixedRowAddMenu = New ContextMenu()
			Dim add As New MenuItem()
			add.Index = 0
			add.Text = "Add to fixed rows"
			AddHandler add.Click, AddressOf add_Click
			fixedRowAddMenu.MenuItems.Add(add)
			fixedRowRemoveMenu = New ContextMenu()
			Dim remove As New MenuItem()
			remove.Index = 0
			remove.Text = "Remove from fixed rows"
			AddHandler remove.Click, AddressOf remove_Click
			fixedRowRemoveMenu.MenuItems.Add(remove)

			'show split
			gridSplitContainer1.ShowSplitView()
			gridSplitContainer1.FixedPanel = SplitFixedPanel.Panel1

			'raise events
			AddHandler gridViewPanel1.CustomRowFilter, AddressOf gridViewPanel1_CustomRowFilter
			AddHandler gridViewPanel1.PopupMenuShowing, AddressOf gridViewPanel1_PopupMenuShowing
			gridViewPanel2 = TryCast(gridSplitContainer1.SplitChildGrid.MainView, GridView)
			AddHandler gridViewPanel2.CustomRowFilter, AddressOf gridViewPanel2_CustomRowFilter
			AddHandler gridViewPanel2.PopupMenuShowing, AddressOf gridViewPanel2_PopupMenuShowing
			AddHandler gridViewPanel1.CalcRowHeight, AddressOf gridViewPanel2_CalcRowHeight
			AddHandler gridViewPanel2.CalcRowHeight, AddressOf gridViewPanel2_CalcRowHeight
			AddHandler gridViewPanel1.ShowingEditor, AddressOf gridViewPanel1_ShowingEditor
			AddHandler gridViewPanel1.RowCellStyle, AddressOf gridViewPanel1_RowCellStyle
			AddHandler gridViewPanel1.Layout, AddressOf gridViewPanel1_Layout
			gridSplitContainer1.Grid.ForceInitialize()
			Dim viewInfo As GridViewInfo = TryCast(gridViewPanel1.GetViewInfo(), GridViewInfo)
			gridSplitContainer1.SplitterPosition = viewInfo.ViewRects.ColumnPanel.Bottom
		End Sub

		Private Sub gridViewPanel1_Layout(ByVal sender As Object, ByVal e As EventArgs)
			RefreshData()
		End Sub

		Private Sub gridViewPanel1_RowCellStyle(ByVal sender As Object, ByVal e As RowCellStyleEventArgs)
			Dim viewInfo As GridViewInfo = TryCast(gridViewPanel1.GetViewInfo(), GridViewInfo)
			e.Appearance.Assign(viewInfo.PaintAppearance.HideSelectionRow)
		End Sub

		Private Sub gridViewPanel2_CalcRowHeight(ByVal sender As Object, ByVal e As RowHeightEventArgs)
			If listOfRowsHeights.Count <> gridViewPanel2.RowCount Then
				listOfRowsHeights.Add(e.RowHeight)
			End If
		End Sub

		Private Sub gridViewPanel1_ShowingEditor(ByVal sender As Object, ByVal e As CancelEventArgs)
			e.Cancel = True
		End Sub

		Private Sub gridViewPanel1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
			Dim gv As GridView = TryCast(sender, GridView)
			hi = gv.CalcHitInfo(e.Point)
			If hi.InRow Then
				sourceIndex = gv.GetDataSourceRowIndex(hi.RowHandle)
				fixedRowRemoveMenu.Show(gv.GridControl, e.Point)
			End If
		End Sub

		Private Sub gridViewPanel2_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
			Dim gv As GridView = TryCast(sender, GridView)
			hi = gv.CalcHitInfo(e.Point)
			If hi.InRow Then
				sourceIndex = gv.GetDataSourceRowIndex(hi.RowHandle)
				rowHeight = listOfRowsHeights(sourceIndex)
				fixedRowAddMenu.Show(gv.GridControl, e.Point)
			End If
		End Sub

		Private Sub gridViewPanel1_CustomRowFilter(ByVal sender As Object, ByVal e As RowFilterEventArgs)
			If (Not FixedRows.ContainsKey(e.ListSourceRow)) Then
				e.Visible = False
				e.Handled = True
			End If
		End Sub

		Private Sub gridViewPanel2_CustomRowFilter(ByVal sender As Object, ByVal e As RowFilterEventArgs)
			If FixedRows.ContainsKey(e.ListSourceRow) Then
				e.Visible = False
				e.Handled = True
			End If
		End Sub

		Private Sub add_Click(ByVal sender As Object, ByVal e As EventArgs)
			If (Not FixedRows.ContainsKey(sourceIndex)) Then
				FixedRows.Add(sourceIndex, rowHeight)
			End If
			RefreshData()
		End Sub

		Private Sub remove_Click(ByVal sender As Object, ByVal e As EventArgs)
			If FixedRows.ContainsKey(sourceIndex) Then
				FixedRows.Remove(sourceIndex)
			End If
			RefreshData()
		End Sub

		Private Sub RefreshData()
			gridSplitContainer1.Grid.MainView.RefreshData()
			gridSplitContainer1.SplitChildGrid.MainView.RefreshData()
			ShiftingSplitter()
		End Sub

		Private Sub ShiftingSplitter()
			Dim viewInfo As GridViewInfo = TryCast(gridViewPanel1.GetViewInfo(), GridViewInfo)
			Dim splitterPosition As Integer = 0
			height = viewInfo.ViewRects.ColumnPanel.Bottom + 2
			If viewInfo.GetGridRowInfo(GridControl.AutoFilterRowHandle) IsNot Nothing Then
				height += viewInfo.CalcRowHeight(viewInfo.GInfo.Graphics, GridControl.AutoFilterRowHandle, -1) + 4
			End If
			splitterPosition = height
			Dim values As Dictionary(Of Integer, Integer).ValueCollection = FixedRows.Values
			For i As Integer = 0 To values.Count - 1
				height += values.ToArray()(i) + 1
			Next i
			splitterPosition = Math.Max(height, splitterPosition)
			If splitterPosition >= viewInfo.ViewRects.EmptyRows.Bottom Then
				splitterPosition -= viewInfo.ViewRects.EmptyRows.Height
			End If
			gridSplitContainer1.SplitterPosition = splitterPosition
			splitterPosition -= viewInfo.ViewRects.EmptyRows.Bottom - viewInfo.ViewRects.EmptyRows.Top
			If viewInfo.ViewRects.EmptyRows.Top <> 0 Then
				gridSplitContainer1.SplitterPosition = splitterPosition
			End If
		End Sub
	End Class
End Namespace