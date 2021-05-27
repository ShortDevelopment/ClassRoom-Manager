Imports Microsoft.Toolkit.Forms.UI.XamlHost

Public Class Form1

    Public ReadOnly Property XamlIsland As WindowsXamlHost

    Public Sub New()

        InitializeComponent()

#Region "Xaml Island"
        XamlIsland = New WindowsXamlHost()
        XamlIsland.Dock = DockStyle.Fill
        Me.Controls.Add(XamlIsland)

        XamlIsland.Child = New UI.MainPage()
#End Region

        WindowState = FormWindowState.Maximized

    End Sub

End Class
