
Imports ClassRoom_Manager.UI.Interop
Imports Windows.System

Namespace Frames

    Public NotInheritable Class BeamerFrame
        Inherits Page

        Private Sub ToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:display"))
        End Sub

        Private Sub ToolPresenter_Click_1(sender As Object, e As RoutedEventArgs)
            Task.Run(Sub()
                         Wow64Interop.RunWithoutRedirect(Sub()
                                                             Process.Start(New ProcessStartInfo() With {
                                                                   .FileName = "C:\Windows\System32\DisplaySwitch.exe",
                                                                   .UseShellExecute = False
                                                               })
                                                         End Sub)
                     End Sub)
        End Sub
    End Class

End Namespace