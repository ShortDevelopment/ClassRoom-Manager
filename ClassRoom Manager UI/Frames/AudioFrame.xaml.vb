
Imports ClassRoom_Manager.UI.Controls
Imports Windows.Devices.Enumeration
Imports Windows.Media.Audio
Imports Windows.System
Imports Windows10Design.Interop

Namespace Frames

    Public NotInheritable Class AudioFrame
        Inherits Page
        Public Property A2DPAudioDevices As New ObservableCollection(Of DeviceInformation)

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

            LoadA2DP()

        End Sub

#Region "A2DP"

        Public Async Sub LoadA2DP()
            Dim watcher = DeviceInformation.CreateWatcher(AudioPlaybackConnection.GetDeviceSelector())
            AddHandler watcher.Added, Async Sub(sender As DeviceWatcher, info As DeviceInformation)
                                          Await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, Sub()
                                                                                                                     A2DPAudioDevices.Add(info)
                                                                                                                     UpdateUIBySelectedItem()
                                                                                                                 End Sub)
                                      End Sub
            AddHandler watcher.Removed, Async Sub(sender As DeviceWatcher, args As DeviceInformationUpdate)
                                            Await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, Sub()
                                                                                                                       A2DPAudioDevices.Remove(A2DPAudioDevices.FirstOrDefault(Function(x) x.Id = args.Id))
                                                                                                                       RemoveDeviceById(args.Id)
                                                                                                                       UpdateUIBySelectedItem()
                                                                                                                   End Sub)
                                        End Sub
            watcher.Start()
        End Sub

        Dim audioPlaybackConnections As New Dictionary(Of String, AudioPlaybackConnection)

        Private Async Sub DeviceListView_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            UpdateUIBySelectedItem()
        End Sub

        Private Async Sub ConnectAppBarButton_Click(sender As Object, e As RoutedEventArgs)
            If DeviceListView.SelectedItem IsNot Nothing Then

                Dim selectedDeviceId = DirectCast(DeviceListView.SelectedItem, DeviceInformation).Id
                If Not audioPlaybackConnections.ContainsKey(selectedDeviceId) Then

                    ' Create the audio playback connection from the selected device id And add it to the dictionary. 
                    ' This will result in allowing incoming connections from the remote device. 
                    Dim playbackConnection = AudioPlaybackConnection.TryCreateFromId(selectedDeviceId)

                    If playbackConnection IsNot Nothing Then
                        ' The device has an available audio playback connection. 
                        AddHandler playbackConnection.StateChanged, Async Sub(connection As AudioPlaybackConnection, args As Object)
                                                                        Await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, Sub()

                                                                                                                                                   If connection.State = AudioPlaybackConnectionState.Closed Then
                                                                                                                                                       A2DPAudioDevices.Remove(A2DPAudioDevices.FirstOrDefault(Function(x) x.Id = connection.DeviceId))
                                                                                                                                                       RemoveDeviceById(connection.DeviceId)
                                                                                                                                                       UpdateUIBySelectedItem()
                                                                                                                                                   End If

                                                                                                                                               End Sub)
                                                                    End Sub

                        Await playbackConnection.StartAsync()
                        Dim result = Await playbackConnection.OpenAsync()

                        If Not result.Status = AudioPlaybackConnectionOpenResultStatus.Success Then
                            Me.ShowErrorDialog(result.Status.ToString(), "")
                        Else
                            audioPlaybackConnections.Add(selectedDeviceId, playbackConnection)

                            UpdateUIBySelectedItem()
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub DisconnectAppBarButton_Click(sender As Object, e As RoutedEventArgs)
            If DeviceListView.SelectedItem IsNot Nothing Then

                Dim selectedDeviceId = DirectCast(DeviceListView.SelectedItem, DeviceInformation).Id
                RemoveDeviceById(selectedDeviceId)

                UpdateUIBySelectedItem()
            End If
        End Sub

        Private Sub RemoveDeviceById(selectedDeviceId As String)
            If audioPlaybackConnections.ContainsKey(selectedDeviceId) Then

                Dim connectionToRemove = audioPlaybackConnections(selectedDeviceId)
                connectionToRemove.Dispose()
                audioPlaybackConnections.Remove(selectedDeviceId)

            End If
        End Sub

        Private Sub UpdateUIBySelectedItem()
            If DeviceListView.SelectedItem IsNot Nothing Then

                Dim selectedDeviceId = DirectCast(DeviceListView.SelectedItem, DeviceInformation).Id
                If Not audioPlaybackConnections.ContainsKey(selectedDeviceId) Then
                    ConnectAppBarButton.IsEnabled = True
                    DisconnectAppBarButton.IsEnabled = False
                Else
                    ConnectAppBarButton.IsEnabled = False
                    DisconnectAppBarButton.IsEnabled = True
                End If

            Else
                ConnectAppBarButton.IsEnabled = False
                DisconnectAppBarButton.IsEnabled = False
            End If
        End Sub

#End Region

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