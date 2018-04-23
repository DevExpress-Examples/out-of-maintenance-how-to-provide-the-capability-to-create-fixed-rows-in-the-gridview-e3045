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
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base

Namespace GridViewFixedRowsSplitPanel
	Public Class GridSplitContainerDescendant
		Inherits GridSplitContainer
		Protected Overrides Function CreateContainerInfo() As DevExpress.XtraEditors.Drawing.SplitContainerViewInfo
			Return New SplitContainerViewInfoDescendant(Me)
		End Function
		Protected Overrides Sub OnSplitterPositionChanged()
			MyBase.OnSplitterPositionChanged()
		End Sub
	End Class
End Namespace
