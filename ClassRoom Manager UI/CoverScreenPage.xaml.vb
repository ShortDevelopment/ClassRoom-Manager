Imports FullTrustUWP.Core.Xaml

Public NotInheritable Class CoverScreenPage
    Inherits Page

    Dim Subclass As XamlWindowSubclass

    Public Sub New()
        InitializeComponent()

        Subclass = XamlWindowSubclass.ForCurrentWindow()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Window.Current.Close()
    End Sub

    Private Sub DragGrid_PointerEntered(sender As Object, e As PointerRoutedEventArgs)
        Subclass.CursorIsInTitleBar = True
    End Sub

    Private Sub DragGrid_PointerExited(sender As Object, e As PointerRoutedEventArgs)
        ' ToDo: Never get's called!
        Subclass.CursorIsInTitleBar = False
    End Sub
End Class
