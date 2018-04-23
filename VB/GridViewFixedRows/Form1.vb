Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace GVFixedRows
	Partial Public Class Form1
		Inherits Form
		Private fixedRowAddMenu, fixedRowRemoveMenu As ContextMenu
		Private dataRow As DataRow

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			gridControlFixedRows1.DataSource = FillTable()

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
			AddHandler gridViewFixedRows1.PopupMenuShowing, AddressOf gridViewFixedRows1_PopupMenuShowing
		End Sub

		Private Sub gridViewFixedRows1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
			Dim gv As GridViewFixedRows = TryCast(sender, GridViewFixedRows)
			Dim hi As GridHitInfo = gv.CalcHitInfo(e.Point)
			If hi.InRow Then
				dataRow = gv.GetDataRow(hi.RowHandle)
				fixedRowAddMenu.Show(gv.GridControl, e.Point)
			ElseIf gv.InFixedRowsPane(e.Point) Then
				dataRow = gv.GetFixedDataRow(e.Point)
				fixedRowRemoveMenu.Show(gv.GridControl, e.Point)
			End If
		End Sub

		Private Sub add_Click(ByVal sender As Object, ByVal e As EventArgs)
			gridViewFixedRows1.AddToFixedRows(dataRow)
		End Sub

		Private Sub remove_Click(ByVal sender As Object, ByVal e As EventArgs)
			gridViewFixedRows1.RemoveFromFixedRows(dataRow)
		End Sub

		Private Function FillTable() As DataTable
			Dim _customersTable As New DataTable()
			Dim col As DataColumn
			Dim row As DataRow

			_customersTable.TableName = "Customers"

			col = New DataColumn()
			col.ColumnName = "Customer"
			col.DataType = System.Type.GetType("System.String")
			_customersTable.Columns.Add(col)

			col = New DataColumn()
			col.ColumnName = "Purchase Price"
			col.DataType = System.Type.GetType("System.Double")
			_customersTable.Columns.Add(col)

			row = _customersTable.NewRow()
			row("Customer") = "Jack Smith"
			row("Purchase Price") = 120
			_customersTable.Rows.Add(row)

			row = _customersTable.NewRow()
			row("Customer") = "John Doe"
			row("Purchase Price") = 350
			_customersTable.Rows.Add(row)

			row = _customersTable.NewRow()
			row("Customer") = "Jane Doe"
			row("Purchase Price") = 71
			_customersTable.Rows.Add(row)

			row = _customersTable.NewRow()
			row("Customer") = "Sam Piter"
			row("Purchase Price") = 43
			_customersTable.Rows.Add(row)

			row = _customersTable.NewRow()
			row("Customer") = "Dolores Patrick"
			row("Purchase Price") = 311
			_customersTable.Rows.Add(row)

			row = _customersTable.NewRow()
			row("Customer") = "Mike Green"
			row("Purchase Price") = 249
			_customersTable.Rows.Add(row)

			row = _customersTable.NewRow()
			row("Customer") = "Dan West"
			row("Purchase Price") = 126
			_customersTable.Rows.Add(row)

			Return _customersTable
		End Function
	End Class
End Namespace
