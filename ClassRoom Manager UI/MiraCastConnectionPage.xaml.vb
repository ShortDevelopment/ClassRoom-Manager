
Imports Windows.Media.Core
Imports Windows.Media.Miracast
Imports Windows.Media.Playback
Imports Windows.UI.Core

Public NotInheritable Class MiraCastConnectionPage
    Inherits Page

    Public ReadOnly Property Connection As MiracastReceiverConnection
    Public ReadOnly Property MediaSource As MediaSource

    Public Sub New(connection As MiracastReceiverConnection, mediaSource As MediaSource)
        Me.Connection = connection
        Me.MediaSource = mediaSource

        InitializeComponent()

        Dim player As New MediaPlayer() With {
                                .Source = mediaSource,
                                .RealTimePlayback = True
                            }
        MediaPlayerControl.SetMediaPlayer(player)
        player.Play()

    End Sub

    Private Sub VolumeToggleAppBarButton_Click(sender As Object, e As RoutedEventArgs)
        MediaPlayerControl.MediaPlayer.IsMuted = Not MediaPlayerControl.MediaPlayer.IsMuted
        If MediaPlayerControl.MediaPlayer.IsMuted Then
            VolumeToggleAppBarButton.Style = Me.Resources("UnmuteToggleButton")
        Else
            VolumeToggleAppBarButton.Style = Me.Resources("MuteToggleButton")
        End If
    End Sub

    Private Sub FullScreenToggleAppBarButton_Click(sender As Object, e As RoutedEventArgs)
        'CurrentWindow.IsFullScreen = Not CurrentWindow.IsFullScreen
        'If CurrentWindow.IsFullScreen Then
        '    FullScreenToggleAppBarButton.Style = Me.Resources("BackToWindowScreenToggleButton")
        'Else
        '    FullScreenToggleAppBarButton.Style = Me.Resources("FullScrenToggleButton")
        'End If
    End Sub

    Private Sub DisconnectAppBarButton_Click(sender As Object, e As RoutedEventArgs)
        Connection.Disconnect(MiracastReceiverDisconnectReason.DisconnectedByUser)
        Connection.Dispose()

        CoreWindow.GetForCurrentThread().Close()
    End Sub

End Class
