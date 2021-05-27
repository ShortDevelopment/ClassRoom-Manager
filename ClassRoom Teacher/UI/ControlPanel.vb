Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Windows.Networking.Connectivity
Imports Windows.Networking.NetworkOperators

Public Class ControlPanel

#Region "Initialize"

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If Me.WindowState = FormWindowState.Minimized Then Me.WindowState = FormWindowState.Normal
        Me.Focus()
        Me.BringToFront()
    End Sub

    ReadOnly Property UserName As String
    ReadOnly Property Running As Boolean
    ReadOnly Property HotSpotEnabled As Boolean

    Public Sub New()
        InitializeComponent()

        DoubleBuffered = True

    End Sub

    Private Sub ControlPanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        _UserName = Environment.UserName + "@" + Environment.MachineName
        UserNameLabel.Text = UserName
        UserProfilePictureBox.Image = UserProfile.CurrentUser.Avatar

        NotifyIcon1.Text = Me.Text
        NotifyIcon1.Icon = Me.Icon

        If Environment.OSVersion.Version.Major < 10 Then
            MsgBox("Dieses Programm funktioniert nur unter Windows 10 vollständig!", MsgBoxStyle.Exclamation)
        End If

        SetupHotSpot()

        If Not HotSpotEnabled Then
            Try
                IPAddressTextBox.Text = $"http://{NetworkManager.Default.GetIPAddress()}:8080/"
            Catch : End Try
        End If

        _Running = True

        Server.Start()
        ListenerThread.IsBackground = True
        ListenerThread.Start()
    End Sub

#End Region

#Region "TcpServer"

    Property Server As New TcpListener(1234)
    Dim ListenerThread As New Thread(Sub()
                                         While Running
                                             Try
                                                 Dim client = Server.AcceptTcpClient() ' Server.BeginAcceptTcpClient() Wäre natürlich schöner 😁
                                                 Dim reader As New StreamReader(client.GetStream())
                                                 Dim writer As New StreamWriter(client.GetStream())
                                                 writer.AutoFlush = True
                                                 Dim protocol = reader.ReadLine()
                                                 If protocol.ToUpper().Contains("HTTP") Then

                                                     Try
                                                         reader.Close()
                                                         writer.Close()
                                                         client.Close()
                                                     Catch : End Try
                                                 ElseIf protocol.ToUpper().Contains("LK") Then
                                                     Dim name = reader.ReadLine()
                                                     Dim pcname = reader.ReadLine()
                                                     Dim tag = New PCData()
                                                     tag.Client = client
                                                     tag.Writer = writer
                                                     tag.Reader = reader
                                                     Me.Invoke(Sub()
                                                                   Dim panel As New ComputerView()
                                                                   panel.Tag = tag
                                                                   panel.ComputerName = pcname
                                                                   panel.UserName = name
                                                                   panel.Dock = DockStyle.Top
                                                                   AddHandler panel.Selected, AddressOf PCSelected
                                                                   PCOverviewPanel.Controls.Add(panel)
                                                                   panel.BringToFront()
                                                               End Sub)
                                                     writer.WriteLine(Environment.UserName)
                                                 End If
                                             Catch : End Try
                                         End While
                                     End Sub)

#End Region

#Region "Remote"

    Public ReadOnly Property CurrentRemoteView As RemoteViewWindow = Nothing
    Public Sub PCSelected(pc As ComputerView)
        Try
            If Not CurrentRemoteView Is Nothing Then
                CurrentRemoteView.Close()
            End If
        Catch : End Try
        Try
            For Each c In PCOverviewPanel.Controls
                Dim cont = CType(c, ComputerView)
                cont.Checked = False
            Next
            Dim data As PCData = pc.Tag
            Try
                data.Writer.WriteLine("keepalive")
                data.Writer.WriteLine("keepalive")
            Catch ex As Exception
                Me.Invoke(Sub()
                              pc.Parent.Controls.Remove(pc)
                              MsgBox("Die Verbindung wurde unerwarted geschlossen", MsgBoxStyle.Exclamation)
                          End Sub)
                Exit Sub
            End Try
            PCOverviewPanel.Enabled = False
            pc.Checked = True
            Dim win = New RemoteViewWindow(data, pc)
            win.Show()
            win.BringToFront()
            AddHandler win.ConnectionEnded, Sub()
                                                For Each c In PCOverviewPanel.Controls
                                                    Dim cont = CType(c, ComputerView)
                                                    cont.Checked = False
                                                Next
                                                PCOverviewPanel.Enabled = True
                                            End Sub
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

#End Region

#Region "HotSpot"
    Dim OldTeatheringConfiguration As NetworkOperatorTetheringAccessPointConfiguration
    Dim OldMaxPersons As Integer
    Public ReadOnly Property HotSpotManager As NetworkOperatorTetheringManager
    Public ReadOnly Property HotSpotIPAddress As IPAddress = Nothing

    Public Sub SetupHotSpot()
        Try
            Dim profile As ConnectionProfile
            Try
                profile = NetworkInformation.GetInternetConnectionProfile()
                If profile Is Nothing Then Throw New Exception
            Catch
                profile = NetworkInformation.GetConnectionProfiles()(0)
            End Try
            _HotSpotManager = NetworkOperatorTetheringManager.CreateFromConnectionProfile(profile)
            OldTeatheringConfiguration = HotSpotManager.GetCurrentAccessPointConfiguration()
            Dim NewConfig As New NetworkOperatorTetheringAccessPointConfiguration()
            NewConfig.Ssid = $"ClassRoom of {Environment.UserName}"
            NewConfig.Passphrase = RandomStringMaker.GetRandomString(8).ToUpper()
            NewConfig.Passphrase = OldTeatheringConfiguration.Passphrase
            NewConfig.Band = TetheringWiFiBand.Auto
            HotSpotManager.ConfigureAccessPointAsync(NewConfig).Completed = Sub()
                                                                                Thread.Sleep(200)
                                                                                Me.Invoke(Sub()
                                                                                              For Each nic As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                                                                                                  If nic.NetworkInterfaceType = NetworkInterfaceType.Wireless80211 AndAlso nic.GetIPProperties().GatewayAddresses.Count = 0 Then ' AndAlso nic.OperationalStatus = OperationalStatus.Up 
                                                                                                      Dim Addresses = nic.GetIPProperties().UnicastAddresses.Where(Function(x) x.Address.AddressFamily = AddressFamily.InterNetwork).ToList()
                                                                                                      If Addresses.Count > 0 Then
                                                                                                          _HotSpotIPAddress = Addresses(0).Address
                                                                                                          If HotSpotIPAddress.ToString().EndsWith(".1") Then
                                                                                                              Debug.Print("IPAddress: " + HotSpotIPAddress.ToString())
                                                                                                              IPAddressTextBox.Text = $"http://{HotSpotIPAddress}:8080/"
                                                                                                              Exit For
                                                                                                          End If
                                                                                                      End If
                                                                                                  End If
                                                                                              Next
                                                                                              SetupHTTPServer()
                                                                                          End Sub)
                                                                            End Sub

            If Not HotSpotManager.TetheringOperationalState = TetheringOperationalState.On Then HotSpotManager.StartTetheringAsync()
            SSIDTextBox.Text = NewConfig.Ssid
            WiFiPasswordTextBox.Text = NewConfig.Passphrase
            _HotSpotEnabled = True
        Catch ex As Exception
            WiFiDisplayPanel.Hide()
            MsgBox("HotSpot:" + vbNewLine + ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
#End Region

#Region "HTTPServer"

    Dim CurrentAssembly As Assembly = Assembly.GetExecutingAssembly()
    Public ReadOnly Property HTTPServer As HttpListener
    Dim HTTPListenerThread As New Thread(Sub()
                                             While Running
                                                 Try
                                                     Dim Context = HTTPServer.GetContext() ' HTTPServer.BeginGetContext() Wäre natürlich schöner 😁
                                                     RunOnNewThread(Sub()
                                                                        Dim Path = Context.Request.Url.LocalPath
                                                                        If Path = "/" Then Path = "/index.html"
                                                                        If SpecialEndpoints.ContainsKey(Path.ToLower()) Then
                                                                            SpecialEndpoints(Path.ToLower())(Context)
                                                                            Exit Sub
                                                                        End If
                                                                        Dim x = CurrentAssembly.GetManifestResourceNames()
                                                                        Using stream = CurrentAssembly.GetManifestResourceStream($"ClassRoom_Teacher.{Path.Substring(1)}")
                                                                            If stream Is Nothing Then
                                                                                Return404Error(Context)
                                                                                Exit Sub
                                                                            End If

                                                                            Context.Response.StatusCode = CType(HttpStatusCode.OK, Integer)
                                                                            If Path.ToLower().EndsWith(".html") Then Context.Response.ContentType = "text/html"
                                                                            Using outstream = Context.Response.OutputStream
                                                                                stream.CopyTo(outstream)
                                                                            End Using
                                                                        End Using
                                                                    End Sub)

                                                 Catch : End Try
                                             End While
                                         End Sub)

    Dim SpecialEndpoints As New Dictionary(Of String, Action(Of HttpListenerContext)) From {
        {
            "/info",
            Sub(context As HttpListenerContext)
                Dim data = "{""teacher"": """ + Environment.UserName + """}"
                context.Response.ContentType = "application/json"
                Using stream = context.Response.OutputStream
                    Using writer As New StreamWriter(stream)
                        writer.Write(data)
                    End Using
                End Using
            End Sub
        },
        {
            "/error404.html",
            AddressOf Return404Error
        },
        {
            "/upload.php",
            Sub(context As HttpListenerContext)
                Dim InputStreamData As String
                Using data As New MemoryStream()
                    Using stream = context.Request.InputStream
                        stream.CopyTo(data)
                        data.Position = 0
                        Using reader As New StreamReader(data, System.Text.Encoding.ASCII)
                            Dim reading As Boolean = True
                            Dim FileName As String
                            Dim FildName As String
                            Dim ContentType As String

                            Dim ReadingContent As Boolean = False

                            Dim CalculateStreamPosition = Function() As Integer
                                                              Dim charPos = CType(reader.GetType().GetField("charPos", BindingFlags.CreateInstance Or BindingFlags.Instance Or BindingFlags.NonPublic).GetValue(reader), Integer)
                                                              Dim charLen = CType(reader.GetType().GetField("charLen", BindingFlags.CreateInstance Or BindingFlags.Instance Or BindingFlags.NonPublic).GetValue(reader), Integer)
                                                              Dim byteBuffer = CType(reader.GetType().GetField("byteBuffer", BindingFlags.CreateInstance Or BindingFlags.Instance Or BindingFlags.NonPublic).GetValue(reader), Byte())
                                                              Dim RealStreamPosition = data.Position - (charLen - charPos)
                                                              'If charLen = charPos AndAlso data.Position = byteBuffer.Length Then RealStreamPosition = charPos
                                                              Return RealStreamPosition
                                                          End Function
                            Dim pos1 = -1
                            While reading
                                If ReadingContent Then
                                    While True

                                        Dim line = reader.ReadLine()
                                        If line Is Nothing OrElse line.StartsWith("--") Then

                                            Dim pos2 = CalculateStreamPosition() - (context.Request.ContentEncoding.GetBytes(line).Length + 4)

                                            Dim buffer(pos2 - pos1 - 1) As Byte
                                            data.Position = pos1
                                            data.Read(buffer, 0, buffer.Length)

                                            Dim FinalPath = Path.Combine(UploadDir, FileName.RemoveInvalidFilePathCharacters())

                                            IO.File.WriteAllBytes(FinalPath, buffer)
                                            Me.Invoke(Sub()
                                                          NotifyIcon1.ShowBalloonTip(1000, "Neue Datei", $"Die Datei ""{Path.GetFileName(FinalPath)}""", ToolTipIcon.Info)
                                                      End Sub)
                                            ReadingContent = False
                                            Exit While
                                        End If
                                    End While
                                    Exit While
                                Else
                                    Dim line = reader.ReadLine()
                                    If line.ToLower().StartsWith("content-disposition:") Then
                                        FileName = Path.GetFileName(Split(Split(line, "filename=""")(1), """")(0))
                                        FildName = Path.GetFileName(Split(Split(line, "name=""")(1), """")(0))
                                    ElseIf line.ToLower().StartsWith("content-type:") Then
                                        ContentType = Split(line.ToLower(), "content-type:")(1).Trim()
                                    ElseIf line = "" Then
                                        ReadingContent = True
                                        pos1 = CalculateStreamPosition()
                                    End If
                                End If
                            End While

                            context.Response.StatusCode = HttpStatusCode.OK
                            context.Response.Close()

                        End Using
                    End Using
                End Using
                Debug.Print(InputStreamData)
            End Sub
        }
    }

    ReadOnly Property UploadDir As String
        Get
            Dim path = IO.Path.Combine(Application.StartupPath, "Uploads")
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            Return path
        End Get
    End Property

    Sub Return404Error(context As HttpListenerContext)

    End Sub

    Public Sub SetupHTTPServer()
        Try
            If Not FirewallUtils.IsPortOpen(8080) Then FirewallUtils.OpenPort(8080, "ClassRoom Teacher")
            _HTTPServer = New HttpListener()
            HTTPServer.Prefixes.Add("http://+:8080/")
            HTTPServer.Start()
            IPAddressTextBox.Show()
            HTTPListenerThread.IsBackground = True
            HTTPListenerThread.Start()
        Catch ex As HttpListenerException
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "UI"

    Private Sub ControlPanel_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Panel9.Location = New Point(Panel9.Parent.Width / 2 - Panel9.Width / 2, Panel9.Parent.Height / 2 - Panel9.Height / 2)
        MenuPanel.Location = New Point(MenuPanel.Parent.Width - MenuPanel.Width, 0)
    End Sub

    Private Sub UserProfilePictureBox_Click(sender As Object, e As EventArgs) Handles UserProfilePictureBox.Click
        If MenuPanel.Visible Then
            MenuPanel.Hide()
        Else
            MenuPanel.Show()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MenuPanel.Hide()
        Process.Start(UploadDir)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button3.Click
        MenuPanel.Hide()
        Process.Start("ms-settings:network-mobilehotspot")
    End Sub

#End Region

#Region "Shutdown"

    Private Sub ControlPanel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        _Running = False

        Try
            Server.Stop()
        Catch : End Try
        Try
            If Not HTTPServer Is Nothing Then
                HTTPServer.Stop()
            End If
        Catch : End Try
        Try
            If Not HotSpotManager Is Nothing Then
                HotSpotManager.ConfigureAccessPointAsync(OldTeatheringConfiguration)
                HotSpotManager.StopTetheringAsync()
            End If
        Catch : End Try
    End Sub

#End Region

End Class

Public Structure PCData
    Public Client As TcpClient
    Public Writer As StreamWriter
    Public Reader As StreamReader
End Structure
