Imports System.Collections.ObjectModel
Imports ClassRoom_Manager.UI.Interop.Application

Public Class Program

    <STAThread>
    Public Shared Sub Main(args As String())

        Dim app As New ApplicationManager(args)

    End Sub

    Public Class ApplicationManager
        Implements IApplicationCore

        Public ReadOnly Property CommandLineArgs As ReadOnlyCollection(Of String)

        Public Sub New(args As String())
            CommandLineArgs = New ReadOnlyCollection(Of String)(args)
            Inititalize()
        End Sub

        Protected Sub Inititalize()
            Using New UI.App(Me)
                Application.Run(New Form1())
            End Using
        End Sub

        Public Function CreateNewWindow() As IApplicationCoreWindow Implements IApplicationCore.CreateNewWindow
            Return New ApplicationCoreWindow()
        End Function
    End Class

End Class
