

Public NotInheritable Class CoverScreenPage
    Inherits Page

    Public ReadOnly Property CurrentWindow As Windows10Design.IApplicationCoreWindow

    Public Sub New(window As Windows10Design.IApplicationCoreWindow)

        InitializeComponent()
        Me.CurrentWindow = window

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        CurrentWindow.Close()
    End Sub
End Class
