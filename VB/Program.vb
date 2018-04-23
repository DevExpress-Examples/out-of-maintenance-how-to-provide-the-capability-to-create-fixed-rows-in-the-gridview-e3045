' Developer Express Code Central Example:
' How to provide the capability to create fixed rows in the GridView
' 
' The current example illustrates how to implement a functionality for creating
' fixed rows, which will be displayed on top of the GridView.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3045

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.UserSkins

Namespace GridViewFixedRowsSplitPanel
	Friend NotInheritable Class Program

		Private Sub New()
		End Sub

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Shared Sub Main()
			BonusSkins.Register()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Form1())
		End Sub
	End Class
End Namespace
