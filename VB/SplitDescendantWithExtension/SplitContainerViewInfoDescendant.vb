Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraEditors.Drawing
Imports System.Drawing

Namespace GridViewFixedRowsSplitPanel
	Public Class SplitContainerViewInfoDescendant
		Inherits SplitContainerViewInfo

		Public Sub New(ByVal container As DevExpress.XtraEditors.SplitContainerControl)
			MyBase.New(container)

		End Sub

		Protected Overrides Sub CalcInfo()
			MyBase.CalcInfo()
		End Sub
		Protected Overrides Sub UpdatePanelBounds()
			MyBase.UpdatePanelBounds()
			Dim rect As Rectangle = Panel2Info.Bounds
			rect.Height += Splitter.Bounds.Height
			rect.Y -= Splitter.Bounds.Height
			Panel2Info.Bounds = rect
			rect = Splitter.Bounds
			rect.Height = 0
			Splitter.Bounds = rect
		End Sub
	End Class
End Namespace
