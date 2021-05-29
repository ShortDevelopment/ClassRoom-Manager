
Imports ClassRoom_Manager.UI.Interop
Imports Windows.System

Namespace Frames

    Public NotInheritable Class AudioFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

        End Sub

#Region "Links"
        Private Sub SoundToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:sound"))
        End Sub

        Private Sub MixerToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:apps-volume"))
        End Sub

        Private Sub SoundOldToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Task.Run(Sub()
                         Wow64Interop.RunWithoutRedirect(Sub()
                                                             Process.Start(New ProcessStartInfo() With {
                                                                   .FileName = "C:\Windows\System32\rundll32.exe",
                                                                   .Arguments = "shell32.dll,Control_RunDLL mmsys.cpl,,sound",
                                                                   .UseShellExecute = False
                                                               })
                                                         End Sub)
                     End Sub)
        End Sub

        Private Sub MixerOldToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Task.Run(Sub()
                         Wow64Interop.RunWithoutRedirect(Sub()
                                                             Process.Start(New ProcessStartInfo() With {
                                                                   .FileName = "C:\Windows\System32\SndVol.exe",
                                                                   .UseShellExecute = False
                                                               })
                                                         End Sub)
                     End Sub)
        End Sub

#End Region

    End Class

End Namespace