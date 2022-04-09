Imports Windows.UI.Core

Public NotInheritable Class CoverScreenPage
    Inherits Page

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        CoreWindow.GetForCurrentThread().Close()
    End Sub
End Class
