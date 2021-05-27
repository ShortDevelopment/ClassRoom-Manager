
Imports Windows.Media.Core
Imports Windows.Media.Miracast
Imports Windows.Media.Playback

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

End Class
