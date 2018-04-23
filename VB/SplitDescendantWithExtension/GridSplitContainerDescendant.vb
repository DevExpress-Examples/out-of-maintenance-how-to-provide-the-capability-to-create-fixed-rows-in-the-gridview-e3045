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
