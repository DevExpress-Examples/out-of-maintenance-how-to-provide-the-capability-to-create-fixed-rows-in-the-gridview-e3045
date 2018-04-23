' Developer Express Code Central Example:
' How to provide the capability to create fixed rows in the GridView
' 
' The current example illustrates how to implement a functionality for creating
' fixed rows, which will be displayed on top of the GridView.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3045

Namespace GridViewFixedRowsSplitPanel
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridSplitContainer1 = New GridViewFixedRowsSplitPanel.GridSplitContainerDescendant()
			Me.gridSplitContainerDescendant1Grid = New DevExpress.XtraGrid.GridControl()
			Me.gridSplitContainerDescendant1View = New DevExpress.XtraGrid.Views.Grid.GridView()
			DirectCast(Me.gridSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.gridSplitContainer1.SuspendLayout()
			DirectCast(Me.gridSplitContainerDescendant1Grid, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.gridSplitContainerDescendant1View, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridSplitContainer1
			' 
			Me.gridSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridSplitContainer1.Grid = Me.gridSplitContainerDescendant1Grid
			Me.gridSplitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.gridSplitContainer1.Name = "gridSplitContainer1"
			Me.gridSplitContainer1.Panel1.Controls.Add(Me.gridSplitContainerDescendant1Grid)
			Me.gridSplitContainer1.Panel1.Text = "Panel1"
			Me.gridSplitContainer1.Panel2.Text = "Panel2"
			Me.gridSplitContainer1.Size = New System.Drawing.Size(705, 394)
			Me.gridSplitContainer1.TabIndex = 0
			Me.gridSplitContainer1.Text = "gridSplitContainerDescendant1"
			' 
			' gridSplitContainerDescendant1Grid
			' 
			Me.gridSplitContainerDescendant1Grid.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridSplitContainerDescendant1Grid.Location = New System.Drawing.Point(0, 0)
			Me.gridSplitContainerDescendant1Grid.MainView = Me.gridSplitContainerDescendant1View
			Me.gridSplitContainerDescendant1Grid.Name = "gridSplitContainerDescendant1Grid"
			Me.gridSplitContainerDescendant1Grid.Size = New System.Drawing.Size(705, 394)
			Me.gridSplitContainerDescendant1Grid.TabIndex = 0
			Me.gridSplitContainerDescendant1Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridSplitContainerDescendant1View})
			' 
			' gridSplitContainerDescendant1View
			' 
			Me.gridSplitContainerDescendant1View.GridControl = Me.gridSplitContainerDescendant1Grid
			Me.gridSplitContainerDescendant1View.Name = "gridSplitContainerDescendant1View"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(705, 394)
			Me.Controls.Add(Me.gridSplitContainer1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load)
			DirectCast(Me.gridSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.gridSplitContainer1.ResumeLayout(False)
			DirectCast(Me.gridSplitContainerDescendant1Grid, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.gridSplitContainerDescendant1View, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private gridSplitContainer1 As GridSplitContainerDescendant
		Private gridSplitContainerDescendant1Grid As DevExpress.XtraGrid.GridControl
		Private gridSplitContainerDescendant1View As DevExpress.XtraGrid.Views.Grid.GridView





	End Class
End Namespace

