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
Imports System.Windows.Forms
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraBars.Helpers

Namespace GridViewFixedRowsSplitPanel
	Partial Public Class Form1
		Inherits Form

		Private listOfCustomers As New BindingList(Of Customers)()

		Private Extension As FixedRowsExtension

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Extension = New FixedRowsExtension(gridSplitContainer1)

			'fill datasource of gridControl1
			gridSplitContainer1.Grid.DataSource = FillTables()
			Dim rilue As New RepositoryItemLookUpEdit()
			rilue.DisplayMember = "Customer"
			rilue.ValueMember = "PurchasePrice"
			rilue.DataSource = FillTables()
			gridSplitContainer1.Grid.RepositoryItems.Add(rilue)
			TryCast(gridSplitContainer1.View, GridView).Columns("PurchasePrice").ColumnEdit = rilue
		End Sub

		Private Function FillTables() As BindingList(Of Customers)
			listOfCustomers.Clear()
			For i As Integer = 0 To 49
				listOfCustomers.Add(New Customers(String.Format("Customer{0}", i + 1), i * 100))
			Next i
			Return listOfCustomers
		End Function
	End Class
End Namespace