Imports Windows.System
Imports Windows10Design.Interop

Namespace Frames

    Public NotInheritable Class BeamerFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

        End Sub

#Region "Links"

        Private Sub DisplayToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:display"))
        End Sub

        Private Sub ProjectToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Task.Run(Sub()
                         Wow64Interop.RunWithoutRedirect(Sub()
                                                             Process.Start(New ProcessStartInfo() With {
                                                                   .FileName = "C:\Windows\System32\DisplaySwitch.exe",
                                                                   .UseShellExecute = False
                                                               })
                                                         End Sub)
                     End Sub)
        End Sub
        Private Sub ConnectToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings-connectabledevices:devicediscovery"))
        End Sub

#End Region

    End Class

End Namespace