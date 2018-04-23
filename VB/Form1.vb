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

		Private _Ds As BindingList(Of Customers)
		Private listOfCustomers As New BindingList(Of Customers)()

		Private Extension As FixedRowsExtension

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Extension = New FixedRowsExtension(gridSplitContainer1)

			'fill datasource of gridControl1
			_Ds = FillTables()
			gridSplitContainer1.Grid.DataSource = _Ds
			gridSplitContainerDescendant1View.OptionsView.ColumnAutoWidth = False

			Dim rilue As New RepositoryItemLookUpEdit()
			rilue.DisplayMember = "Customer"
			rilue.ValueMember = "PurchasePrice"
			rilue.DataSource = _Ds
			gridSplitContainer1.Grid.RepositoryItems.Add(rilue)
			TryCast(gridSplitContainer1.View, GridView).Columns("PurchasePrice").ColumnEdit = rilue
		End Sub

		Private Function FillTables() As BindingList(Of Customers)
			listOfCustomers.Clear()
			For i As Integer = 0 To 29
				listOfCustomers.Add(New Customers(String.Format("Customer{0}", i + 1), i * 100))
			Next i
			Return listOfCustomers
		End Function
	End Class
End Namespace