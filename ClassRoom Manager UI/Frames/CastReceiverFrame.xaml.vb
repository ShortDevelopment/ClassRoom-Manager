Imports Windows.Media.Miracast
Imports Windows.System
Imports ClassRoom_Manager.UI.Controls
Imports Windows.Media.Playback
Imports Windows.Media.Core

Namespace Frames

    Public NotInheritable Class CastReceiverFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

            ConnectionsListView.ItemsSource = Connections

            LoadMiracast()

            Dim player As New MediaPlayer() With {
                                    .Source = MediaSource.CreateFromUri(New Uri("file:///D:\Aufnahmen\Render Test v1.mp4"))
                                    }
            MediaPlayerControl.SetMediaPlayer(player)
            player.Play()

        End Sub

#Region "MiraCast"
        Public ReadOnly Property Server As MiracastReceiver
        Public ReadOnly Property CurrentSession As MiracastReceiverSession

        Public ReadOnly Property Connections As New ObservableCollection(Of MiracastReceiverConnection)
        Public ReadOnly Property ConnectionPins As New List(Of String)
        Public ReadOnly Property CurrentConnection As MiracastReceiverConnection

        Public Async Sub LoadMiracast()
            _Server = New MiracastReceiver()
            Dim globalSettings = Server.GetCurrentSettings()
            Dim settings = Server.GetDefaultSettings()
            settings.FriendlyName = globalSettings.FriendlyName
            Server.DisconnectAllAndApplySettings(settings)
            DeviceNameTextBox.Text = settings.FriendlyName
        End Sub

        Private Sub SaveDeviceNameButton_Click(sender As Object, e As RoutedEventArgs)
            Dim globalSettings = Server.GetCurrentSettings()
            Dim settings = Server.GetDefaultSettings()
            settings.FriendlyName = DeviceNameTextBox.Text
            Dim result = Server.DisconnectAllAndApplySettings(settings)

            If Not result.Status = MiracastReceiverApplySettingsStatus.Success Then
                Me.ShowErrorDialog(result.Status.ToString(), result.ExtendedError.Message)
            End If
        End Sub

        Dim MiraCastEnabledToggleSwitch_Toggled_Internal As Boolean = False
        Private Async Sub MiraCastEnabledToggleSwitch_Toggled(sender As Object, e As RoutedEventArgs)
            If MiraCastEnabledToggleSwitch_Toggled_Internal Then
                MiraCastEnabledToggleSwitch_Toggled_Internal = False
                Exit Sub
            End If

            If MiraCastEnabledToggleSwitch.IsOn Then
                _CurrentSession = Server.CreateSession(Nothing)
                CurrentSession.AllowConnectionTakeover = False
                AddHandler CurrentSession.ConnectionCreated, Sub(session As MiracastReceiverSession, args As MiracastReceiverConnectionCreatedEventArgs)
                                                                 Connections.Add(args.Connection)
                                                                 ConnectionPins.Add(args.Pin)
                                                             End Sub
                AddHandler CurrentSession.MediaSourceCreated, Sub(session As MiracastReceiverSession, args As MiracastReceiverMediaSourceCreatedEventArgs)
                                                                  Dim player As New MediaPlayer() With {
                                                                    .Source = args.MediaSource,
                                                                    .RealTimePlayback = True
                                                                  }
                                                                  MediaPlayerControl.SetMediaPlayer(player)
                                                                  player.Play()
                                                                  _CurrentConnection = args.Connection
                                                              End Sub
                AddHandler CurrentSession.Disconnected, Sub(session As MiracastReceiverSession, args As MiracastReceiverDisconnectedEventArgs)
                                                            Dim item = Connections.Where(Function(x) x.Transmitter.MacAddress = args.Connection.Transmitter.MacAddress).FirstOrDefault()
                                                            If item IsNot Nothing Then
                                                                Connections.Remove(item)
                                                                ConnectionPins.RemoveAt(Connections.IndexOf(item))
                                                            End If
                                                        End Sub
                Dim result = Await CurrentSession.StartAsync()

                If Not result.Status = MiracastReceiverSessionStartStatus.Success Then
                    MiraCastEnabledToggleSwitch_Toggled_Internal = True
                    MiraCastEnabledToggleSwitch.IsOn = False

                    Me.ShowErrorDialog(result.Status.ToString(), result.ExtendedError.Message)
                End If
            Else
                If Not CurrentSession Is Nothing Then
                    CurrentSession.Dispose()
                End If
            End If

        End Sub

        Private Sub FullScreenAppBarButton_Tapped(sender As Object, e As TappedRoutedEventArgs)
            MediaPlayerControl.IsFullWindow = True
        End Sub

        Private Sub DisconnectAppBarButton_Tapped(sender As Object, e As TappedRoutedEventArgs)
            CurrentConnection.Disconnect(MiracastReceiverDisconnectReason.DisconnectedByUser)
        End Sub

#End Region

#Region "Links"

        Private Sub MiracastToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:project"))
        End Sub

#End Region
    End Class

End Namespace