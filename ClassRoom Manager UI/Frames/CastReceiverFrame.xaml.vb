
Imports ClassRoom_Manager.UI.Interop
Imports Windows.System

Namespace Frames

    Public NotInheritable Class CastReceiverFrame
        Inherits Page

        Private Sub MiracastToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:project"))
        End Sub
    End Class

End Namespace