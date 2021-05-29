
''' <summary>
''' Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
''' </summary>
Public NotInheritable Class CoverScreenPage
    Inherits Page

    Public ReadOnly Property CurrentWindow As Interop.Application.IApplicationCoreWindow

    Public Sub New(window As Interop.Application.IApplicationCoreWindow)

        InitializeComponent()
        Me.CurrentWindow = window

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        CurrentWindow.Close()
    End Sub
End Class
