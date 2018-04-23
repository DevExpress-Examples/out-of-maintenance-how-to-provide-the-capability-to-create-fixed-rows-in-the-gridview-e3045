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
Imports System.Text

Namespace GridViewFixedRowsSplitPanel
	Friend Class Customers
		Public Sub New(ByVal customer As String, ByVal price As Double)
			Me.Customer = customer
			PurchasePrice = price
		End Sub
		' Fields...
		Private _Price As Integer
		Private _PurchasePrice As Double
		Private _Customer As String

		Public Property Customer() As String
			Get
				Return _Customer
			End Get
			Set(ByVal value As String)
				_Customer = value
			End Set
		End Property

		Public Property PurchasePrice() As Double
			Get
				Return _PurchasePrice
			End Get
			Set(ByVal value As Double)
				_PurchasePrice = value
			End Set
		End Property

		Public Property Price() As Integer
			Get
				Return _Price
			End Get
			Set(ByVal value As Integer)
				_Price = value
			End Set
		End Property
	End Class
End Namespace
