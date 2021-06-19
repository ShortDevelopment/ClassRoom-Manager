Imports Windows10Design

Public Class Program
    Inherits Win32Application

    <STAThread>
    Public Shared Sub Main(args As String())

        Dim app As New Program(args)
        Dim window As New Form1()
        app.Run(New Form1())

    End Sub

    Public Sub New(args As String())
        MyBase.New(args)
    End Sub

    Public Overrides Function CreateUWPApplication() As IDisposable
        Return New UI.App(Me)
    End Function

    Public Overrides Function GetXamlContent() As Object
        Return New UI.MainPage()
    End Function
End Class
