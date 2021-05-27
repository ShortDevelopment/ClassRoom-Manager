
Imports ClassRoom_Manager.UI.Interop
Imports Windows.System

Namespace Frames

    Public NotInheritable Class HotSpotFrame
        Inherits Page

        Private Sub ToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:network-mobilehotspot"))
        End Sub

        Private Sub ToolPresenter_Click_1(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:network-wifi"))
        End Sub

        Private Sub ToolPresenter_Click_2(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:network-ethernet"))
        End Sub
    End Class

End Namespace