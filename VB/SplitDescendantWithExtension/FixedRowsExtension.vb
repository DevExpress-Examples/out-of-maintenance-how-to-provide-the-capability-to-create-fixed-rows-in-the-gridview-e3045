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
Imports DevExpress.XtraGrid.Scrolling

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
		Private panel2State As ScrollBarPresence = ScrollBarPresence.Unknown
		Private gridControl1 As New GridControl()
		Private gridSplitContainer1 As GridSplitContainerDescendant

		Public Sub New(ByVal split As GridSplitContainerDescendant)
			gridSplitContainer1 = split
			gridControl1 = TryCast(gridSplitContainer1.Grid, GridControl)
			gridControl1.ForceInitialize()
			gridViewPanel1 = TryCast(gridControl1.MainView, GridView)
			gridViewPanel1.OptionsMenu.ShowSplitItem = False
			gridViewPanel1.OptionsCustomization.AllowGroup = False

			Dim scrollBar = gridControl1.Controls.OfType(Of Control)().FirstOrDefault(Function(x) TypeOf x Is VCrkScrollBar)
				If scrollBar IsNot Nothing Then
					gridControl1.Controls.Remove(scrollBar)
				End If

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
			AddHandler gridViewPanel1.Layout, AddressOf gridViewPanel1_Layout
			gridSplitContainer1.Grid.ForceInitialize()
			AddHandler gridViewPanel1.FocusedRowChanged, AddressOf gridViewPanel1_FocusedRowChanged
			AddHandler gridSplitContainer1.Panel2.SizeChanged, AddressOf Panel2_SizeChanged
			AddHandler gridViewPanel2.RowCountChanged, AddressOf gridViewPanel2_RowCountChanged
			Dim viewInfo1 As GridViewInfo = TryCast(gridViewPanel1.GetViewInfo(), GridViewInfo)
			gridSplitContainer1.SplitterPosition = viewInfo1.ViewRects.ColumnPanel.Bottom
			Dim viewInfo2 As GridViewInfo = TryCast(gridViewPanel2.GetViewInfo(), GridViewInfo)
			panel2State = viewInfo2.VScrollBarPresence
		End Sub

		Private Sub gridViewPanel2_RowCountChanged(ByVal sender As Object, ByVal e As EventArgs)
			ColumnSynk()
		End Sub


		Private Sub gridViewPanel1_FocusedRowChanged(ByVal sender As Object, ByVal e As FocusedRowChangedEventArgs)
			If e.FocusedRowHandle = (TryCast(sender, GridView)).RowCount - 1 AndAlso (TryCast(gridViewPanel2.GetViewInfo(), GridViewInfo)).VScrollBarPresence = ScrollBarPresence.Hidden Then
				TryCast(sender, GridView).RowHeight += 1
				RefreshData()
			End If
		End Sub

		Private Sub ColumnSynk()
			Dim viewInfo2 As GridViewInfo = TryCast(gridViewPanel2.GetViewInfo(), GridViewInfo)
            If viewInfo2.VScrollBarPresence <> panel2State Then
                panel2State = viewInfo2.VScrollBarPresence
                If viewInfo2.VScrollBarPresence = ScrollBarPresence.Visible Then
                    gridViewPanel1.VertScrollVisibility = ScrollVisibility.Always
                    RefreshData()
                Else
                    If FixedRows.Count <> 0 OrElse gridViewPanel1.VertScrollVisibility <> ScrollVisibility.Auto Then
                        gridViewPanel1.VertScrollVisibility = ScrollVisibility.Never
                    End If
                End If
            End If
        End Sub

		Private Sub Panel2_SizeChanged(ByVal sender As Object, ByVal e As EventArgs)
			ColumnSynk()
		End Sub

		Private Sub gridViewPanel1_Layout(ByVal sender As Object, ByVal e As EventArgs)
			ColumnSynk()
			RefreshData()
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
			If Not FixedRows.ContainsKey(e.ListSourceRow) Then
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
			If Not FixedRows.ContainsKey(sourceIndex) Then
				FixedRows.Add(sourceIndex, rowHeight)
			End If
			If (TryCast(gridViewPanel2.GetViewInfo(), GridViewInfo)).VScrollBarPresence = ScrollBarPresence.Visible Then
				gridViewPanel1.VertScrollVisibility = ScrollVisibility.Always
			Else
				gridViewPanel1.VertScrollVisibility = ScrollVisibility.Never
			End If
			RefreshData()
		End Sub

		Private Sub remove_Click(ByVal sender As Object, ByVal e As EventArgs)
			If FixedRows.ContainsKey(sourceIndex) Then
				FixedRows.Remove(sourceIndex)
			End If
			If FixedRows.Count = 0 AndAlso (TryCast(gridViewPanel2.GetViewInfo(), GridViewInfo)).VScrollBarPresence <> ScrollBarPresence.Visible Then
				gridViewPanel1.VertScrollVisibility = ScrollVisibility.Never
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
				height += viewInfo.CalcRowHeight(viewInfo.GInfo.Graphics, GridControl.AutoFilterRowHandle, -1, 0) + 4
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