Imports System.IO
Imports System.IO.Compression
Imports System.Net.Sockets
Imports System.Threading

Public Class RemoteViewWindow

    Public ReadOnly Property ClientData As PCData
    Public ReadOnly Property Running As Boolean = True

    Dim pc As ComputerView

    Public Sub New(data As PCData, pc As ComputerView)
        InitializeComponent()

        DoubleBuffered = True

        Me._ClientData = data
        Me.pc = pc

        Label3.Text = "Bildschirm von " + pc.UserName

    End Sub

    Dim ListenerThread As New Thread(Sub()
                                         Try
                                             While Running
                                                 Dim cmd = ClientData.Reader.ReadLine()
                                                 If cmd = "shutdown" Then
                                                     MsgBox("Der Schüler hat die Verbindung beendet", MsgBoxStyle.Information)
                                                     Me.Invoke(Sub()
                                                                   pc.Parent.Controls.Remove(pc)
                                                                   Me.Close()
                                                               End Sub)
                                                 ElseIf cmd.StartsWith("img:") Then
                                                     Dim data As Byte() = Convert.FromBase64String(Split(cmd, ":")(1))
                                                     Dim t As New Thread(Sub()
                                                                             Try
                                                                                 Using stream As New MemoryStream(data)
                                                                                     Using stream2 As New MemoryStream
                                                                                         Using gzipstream As New GZipStream(stream, CompressionMode.Decompress)
                                                                                             gzipstream.CopyTo(stream2)
                                                                                         End Using
                                                                                         Me.Invoke(Sub() PictureBox1.Image = Image.FromStream(stream2))
                                                                                     End Using
                                                                                 End Using
                                                                             Catch : End Try
                                                                         End Sub)
                                                     t.IsBackground = True
                                                     t.Start()
                                                 End If
                                             End While
                                         Catch ex As ThreadAbortException
                                         Catch ex As Exception
                                             Try
                                                 Me.Invoke(Sub()
                                                               pc.Parent.Controls.Remove(pc)
                                                               MsgBox(ex.Message, MsgBoxStyle.Critical)
                                                               Me.Close()
                                                           End Sub)
                                             Catch : End Try
                                         End Try
                                     End Sub)

    Private Sub RemoteViewWindow_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ClientData.Writer.AutoFlush = True
            ClientData.Writer.WriteLine("start")
            ListenerThread.IsBackground = True
            ListenerThread.Start()
        Catch ex As Exception
            Try
                Me.Invoke(Sub()
                              pc.Parent.Controls.Remove(pc)
                              MsgBox(ex.Message, MsgBoxStyle.Critical)
                              Me.Close()
                          End Sub)
            Catch : End Try
        End Try
    End Sub

    Public Event ConnectionEnded()

    Private Sub RemoteViewWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _Running = False
        Try
            ClientData.Writer.WriteLine("stop")
        Catch : End Try
        ListenerThread.Abort()
        RaiseEvent ConnectionEnded()
    End Sub

    Private Sub Button1_Click(sender As Button, e As EventArgs) Handles Button1.Click
        If sender.Tag = "full" Then
            sender.Tag = "exit"
            sender.BackgroundImage = My.Resources.baseline_fullscreen_exit_black_18dp
            Me.FormBorderStyle = FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
        Else
            sender.Tag = "full"
            sender.BackgroundImage = My.Resources.baseline_fullscreen_black_18dp
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub RemoteViewWindow_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Panel2.Location = New Point(Panel2.Parent.Width / 2 - Panel2.Width / 2, 0)
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Try
            ClientData.Writer.WriteLine($"scale:{TrackBar1.Value}")
        Catch ex As Exception
            Try
                Me.Invoke(Sub()
                              pc.Parent.Controls.Remove(pc)
                              MsgBox(ex.Message, MsgBoxStyle.Critical)
                              Me.Close()
                          End Sub)
            Catch : End Try
        End Try
    End Sub
End Class