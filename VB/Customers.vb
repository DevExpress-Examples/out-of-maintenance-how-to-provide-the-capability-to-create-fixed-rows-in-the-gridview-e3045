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
