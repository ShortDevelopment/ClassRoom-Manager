Imports System.IO
Imports System.Net.Sockets
Imports System.Threading
Imports Windows.Networking.Connectivity
Imports Windows.Networking.NetworkOperators

Public Class ControlPanel

    Public Sub New()
        InitializeComponent()

        DoubleBuffered = True

    End Sub

    Property UserName As String
    Property Running As Boolean

    Property Server As New TcpListener(1234)
    Dim ListenerThread As New Thread(Sub()
                                         While Running
                                             Try
                                                 Dim client = Server.AcceptTcpClient()
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

    Dim OldTeatheringConfiguration As NetworkOperatorTetheringAccessPointConfiguration
    Dim OldMaxPersons As Integer
    Public ReadOnly Property HotSpotManager As NetworkOperatorTetheringManager

    Private Sub ControlPanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        UserName = Environment.UserName + "@" + Environment.MachineName
        UserNameLabel.Text = UserName
        UserProfilePictureBox.Image = UserProfile.CurrentUser.Avatar

        Dim profile = NetworkInformation.GetInternetConnectionProfile()
        _HotSpotManager = NetworkOperatorTetheringManager.CreateFromConnectionProfile(profile)
        OldTeatheringConfiguration = HotSpotManager.GetCurrentAccessPointConfiguration()
        Dim NewConfig As New NetworkOperatorTetheringAccessPointConfiguration()
        NewConfig.Ssid = $"ClassRoom of {Environment.UserName}"
        NewConfig.Passphrase = RandomStringMaker.GetRandomString(8).ToUpper()
        NewConfig.Band = TetheringWiFiBand.Auto
        HotSpotManager.ConfigureAccessPointAsync(NewConfig)
        HotSpotManager.StartTetheringAsync()

        Running = True
        Server.Start()
        ListenerThread.IsBackground = True
        ListenerThread.Start()
    End Sub

    Private Sub ControlPanel_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Panel9.Location = New Point(Panel9.Parent.Width / 2 - Panel9.Width / 2, Panel9.Parent.Height / 2 - Panel9.Height / 2)
    End Sub

    Private Sub ControlPanel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            HotSpotManager.ConfigureAccessPointAsync(OldTeatheringConfiguration)
            HotSpotManager.StopTetheringAsync()
        Catch : End Try
    End Sub
End Class

Public Structure PCData
    Public Client As TcpClient
    Public Writer As StreamWriter
    Public Reader As StreamReader
End Structure
