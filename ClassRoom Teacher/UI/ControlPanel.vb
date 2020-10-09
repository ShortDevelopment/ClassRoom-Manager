Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Threading
Imports Windows.Networking.Connectivity
Imports Windows.Networking.NetworkOperators

Public Class ControlPanel

#Region "Initialize"

    Property UserName As String
    Property Running As Boolean

    Public Sub New()
        InitializeComponent()

        DoubleBuffered = True

    End Sub

    Private Sub ControlPanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        UserName = Environment.UserName + "@" + Environment.MachineName
        UserNameLabel.Text = UserName
        UserProfilePictureBox.Image = UserProfile.CurrentUser.Avatar

        If Environment.OSVersion.Version.Major < 10 Then
            MsgBox("Dieses Programm funktioniert nur unter Windows 10 vollständig!", MsgBoxStyle.Exclamation)
        End If

        Dim HotSpotEnabled As Boolean = False

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
            NewConfig.Band = TetheringWiFiBand.Auto
            HotSpotManager.ConfigureAccessPointAsync(NewConfig)
            If Not HotSpotManager.TetheringOperationalState = TetheringOperationalState.On Then HotSpotManager.StartTetheringAsync()
            SSIDTextBox.Text = NewConfig.Ssid
            WiFiPasswordTextBox.Text = NewConfig.Passphrase
            HotSpotEnabled = True
        Catch ex As Exception
            WiFiDisplayPanel.Hide()
            MsgBox("HotSpot:" + vbNewLine + ex.Message, MsgBoxStyle.Exclamation)
        End Try

        If HotSpotEnabled Then
            For Each nic As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                If nic.NetworkInterfaceType = NetworkInterfaceType.Wireless80211 AndAlso nic.GetIPProperties().GatewayAddresses.Count = 0 Then ' AndAlso nic.OperationalStatus = OperationalStatus.Up 
                    _HotSpotIPAddress = nic.GetIPProperties().UnicastAddresses.Where(Function(x) x.Address.AddressFamily = AddressFamily.InterNetwork).ToList()(0).Address
                    If HotSpotIPAddress.ToString().EndsWith(".1") Then
                        Debug.Print("IPAddress: " + HotSpotIPAddress.ToString())
                        IPAddressTextBox.Text = $"http://{HotSpotIPAddress}:8080/"
                        SetupHTTPServer()
                        Exit For
                    End If
                End If
            Next
        End If

        Running = True
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

#Region "HotSpot (Settings)"
    Dim OldTeatheringConfiguration As NetworkOperatorTetheringAccessPointConfiguration
    Dim OldMaxPersons As Integer
    Public ReadOnly Property HotSpotManager As NetworkOperatorTetheringManager
    Public ReadOnly Property HotSpotIPAddress As IPAddress = Nothing
#End Region

#Region "HTTPServer"

    Dim CurrentAssembly As Assembly = Assembly.GetExecutingAssembly()
    Public ReadOnly Property HTTPServer As HttpListener
    Dim HTTPListenerThread As New Thread(Sub()
                                             While Running
                                                 Try
                                                     Dim Context = HTTPServer.GetContext() ' HTTPServer.BeginGetContext() Wäre natürlich schöner 😁
                                                     Dim Path = Context.Request.Url.LocalPath
                                                     If Path = "upload.php" Then
                                                         FileUpload(Context)
                                                         Continue While
                                                     End If
                                                     Dim x = CurrentAssembly.GetManifestResourceNames()
                                                     Using stream = CurrentAssembly.GetManifestResourceStream($"ClassRoom_Teacher.{Path.Substring(1)}")
                                                         If stream Is Nothing Then
                                                             Return404Error(Context)
                                                             Continue While
                                                         End If

                                                         Context.Response.StatusCode = CType(HttpStatusCode.OK, Integer)
                                                         If Path.ToLower().EndsWith(".html") Then Context.Response.ContentType = "text/html"
                                                         Using outstream = Context.Response.OutputStream
                                                             stream.CopyTo(outstream)
                                                         End Using
                                                     End Using
                                                 Catch : End Try
                                             End While
                                         End Sub)

    Sub Return404Error(ByRef context As HttpListenerContext)

    End Sub

    Sub FileUpload(ByRef context As HttpListenerContext)

    End Sub

    Public Sub SetupHTTPServer(Optional retry As Boolean = False)
        Try
            _HTTPServer = New HttpListener()
            HTTPServer.Prefixes.Add("http://*:8080/")
            HTTPServer.Start()
            IPAddressTextBox.Show()
            HTTPListenerThread.IsBackground = True
            HTTPListenerThread.Start()
        Catch ex As System.Net.HttpListenerException
            If retry Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            Else
                Dim t As New Thread(Sub()
                                        Try
                                            Dim pi As New ProcessStartInfo()
                                            pi.FileName = "cmd.exe"
                                            pi.Arguments = $"/K netsh http add urlacl url=""http://*:8080/"" user=""Jeder""" ' https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/configuring-http-and-https#configuring-namespace-reservations
                                            pi.Verb = "runas"
                                            Dim p = Process.Start(pi)
                                            p.WaitForExit()
                                            Me.Invoke(Sub() SetupHTTPServer(True))
                                        Catch ex2 As Exception

                                        End Try
                                    End Sub)
                t.IsBackground = True
                t.Start()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "UI"

    Private Sub ControlPanel_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Panel9.Location = New Point(Panel9.Parent.Width / 2 - Panel9.Width / 2, Panel9.Parent.Height / 2 - Panel9.Height / 2)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start("ms-settings:network-mobilehotspot")
    End Sub

#End Region

#Region "Shutdown"

    Private Sub ControlPanel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Running = False

        Try
            Server.Stop()
        Catch : End Try
        Try
            HTTPServer.Stop()
        Catch : End Try
        Try
            HotSpotManager.ConfigureAccessPointAsync(OldTeatheringConfiguration)
            HotSpotManager.StopTetheringAsync()
        Catch : End Try
    End Sub

#End Region

End Class

Public Structure PCData
    Public Client As TcpClient
    Public Writer As StreamWriter
    Public Reader As StreamReader
End Structure
