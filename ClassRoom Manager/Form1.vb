Imports Microsoft.Toolkit.Forms.UI.XamlHost

Public Class Form1
    Inherits ApplicationCoreWindow

    Public Sub New()
        'MyBase.New(GetType(UI.MainPage))

        Content = New UI.MainPage()

        WindowState = FormWindowState.Maximized
    End Sub

End Class
