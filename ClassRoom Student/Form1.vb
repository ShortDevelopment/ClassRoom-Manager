Imports System.Drawing.Imaging
Imports System.IO
Imports System.IO.Compression
Imports System.Net.Sockets
Imports System.Threading

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Image = UserProfile.DefaultAvatar
        UserNameLabel.Text = Environment.UserName
        ComputerNameLabel.Text = Environment.MachineName

        NotifyIcon1.Icon = Me.Icon
        NotifyIcon1.Visible = True

    End Sub

    Dim Writer As StreamWriter
    Dim Reader As StreamReader
    Public ReadOnly Property Client As TcpClient

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Writer.WriteLine("shutdown")
        Catch : End Try
        Running = False
        _Sending = False
        Try
            Writer.Dispose()
            Reader.Dispose()
            Client.Close()
        Catch : End Try
        Try
            ListenerThread.Abort()
        Catch : End Try
        MsgBox("Die Verbindung wurde beendet", MsgBoxStyle.Information)
    End Sub

    Private Sub Button2_Click() Handles Button2.Click
        Dim ip As String = TextBox1.Text
        Panel3.Enabled = False
        Try
            Dim tcpclient As New TcpClient
            tcpclient.Connect(ip, 1234)
            _Client = tcpclient
            Writer = New StreamWriter(Client.GetStream())
            Writer.AutoFlush = True
            Writer.WriteLine("LK ClassRoom Remote Protocol V1")
            Writer.WriteLine(Environment.UserName)
            Writer.WriteLine(Environment.MachineName)
            Reader = New StreamReader(Client.GetStream())
            StatusLabel.Text = "Lehrer: " + Reader.ReadLine()
            ListenerThread.IsBackground = True
            ListenerThread.Start()
            SendingThread.IsBackground = True
            SendingThread.Start()
        Catch ex As Exception
            Panel3.Enabled = True
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Dim Running As Boolean = True

    Dim ListenerThread As New Thread(Sub()
                                         Try
                                             While Running
                                                 Dim cmd = Reader.ReadLine()
                                                 If cmd = "shutdown" Then
                                                     Me.Invoke(Sub() Me.Close())
                                                 ElseIf cmd.StartsWith("scale:") Then
                                                     Dim scale = Int(Split(cmd, ":")(1))
                                                     Select Case scale
                                                         Case 0
                                                             _QualityScalingFactor = 0
                                                         Case 1
                                                             _QualityScalingFactor = 1
                                                         Case 2
                                                             _QualityScalingFactor = 1.3
                                                         Case 3
                                                             _QualityScalingFactor = 1.5
                                                         Case 4
                                                             _QualityScalingFactor = 1.7
                                                         Case 5
                                                             _QualityScalingFactor = 2
                                                     End Select
                                                 ElseIf cmd = "start" Then
                                                     Me.Invoke(Sub()
                                                                   _Sending = True
                                                                   NotifyIcon1.ShowBalloonTip(1000, "Start", "Dein Lehrer hat die Bildschirmübertragung gestarted!", ToolTipIcon.Info)
                                                               End Sub)
                                                 ElseIf cmd = "stop" Then
                                                     Me.Invoke(Sub()
                                                                   _Sending = False
                                                                   NotifyIcon1.ShowBalloonTip(1000, "Stop", "Dein Lehrer hat die Bildschirmübertragung gestoppt!", ToolTipIcon.Info)
                                                               End Sub)
                                                 End If
                                             End While
                                         Catch ex As ThreadAbortException
                                         Catch ex As Exception
                                             Me.Invoke(Sub() Me.Close())
                                             Me.Invoke(Sub() MsgBox(ex.Message, MsgBoxStyle.Critical))
                                         End Try
                                     End Sub)

    Public ReadOnly Property QualityScalingFactor As Single = 1

    Public ReadOnly Property Sending As Boolean
    Dim SendingThread As New Thread(Sub()
                                        While True
                                            While Sending
                                                Try
                                                    Using img = Screen.AllScreens(0).GetScreenshot(QualityScalingFactor)
                                                        Using stream As New MemoryStream
                                                            Dim JPEGEncoder = ImageCodecInfo.GetImageEncoders().First(Function(c) c.FormatID = ImageFormat.Jpeg.Guid)
                                                            Dim encParams = New EncoderParameters() With {
                                                                .Param = {New EncoderParameter(Encoder.Quality, 80L), New EncoderParameter(Encoder.Compression, 100L)}
                                                            }
                                                            img.Save(stream, JPEGEncoder, encParams)
                                                            'img.Save(stream, ImageFormat.Jpeg)
                                                            Using stream2 As New MemoryStream()
                                                                Using gzipstream As New GZipStream(stream2, CompressionMode.Compress, True) 'DeflateStream
                                                                    stream.Position = 0
                                                                    stream.CopyTo(gzipstream)
                                                                End Using
                                                                Dim data = Convert.ToBase64String(stream2.ToArray())
                                                                Writer.WriteLine("img:" + data)
                                                            End Using
                                                        End Using
                                                    End Using
                                                Catch : End Try
                                            End While
                                        End While
                                    End Sub)

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            Button2_Click()
        End If
    End Sub
End Class
