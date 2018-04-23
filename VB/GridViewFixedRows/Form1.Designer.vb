Imports Microsoft.VisualBasic
Imports System
Namespace GVFixedRows
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
			Me.gridControlFixedRows1 = New GVFixedRows.GridControlFixedRows()
			Me.gridViewFixedRows1 = New GVFixedRows.GridViewFixedRows()
			CType(Me.gridControlFixedRows1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridViewFixedRows1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControlFixedRows1
			' 
			Me.gridControlFixedRows1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControlFixedRows1.Location = New System.Drawing.Point(0, 0)
			Me.gridControlFixedRows1.MainView = Me.gridViewFixedRows1
			Me.gridControlFixedRows1.Name = "gridControlFixedRows1"
			Me.gridControlFixedRows1.Size = New System.Drawing.Size(284, 186)
			Me.gridControlFixedRows1.TabIndex = 0
			Me.gridControlFixedRows1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridViewFixedRows1})
			' 
			' gridViewFixedRows1
			' 
			Me.gridViewFixedRows1.GridControl = Me.gridControlFixedRows1
			Me.gridViewFixedRows1.Name = "gridViewFixedRows1"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(284, 186)
			Me.Controls.Add(Me.gridControlFixedRows1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControlFixedRows1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridViewFixedRows1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private gridControlFixedRows1 As GridControlFixedRows
		Private gridViewFixedRows1 As GridViewFixedRows


	End Class
End Namespace

