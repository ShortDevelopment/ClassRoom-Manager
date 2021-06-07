Imports ClassRoom_Manager.UI.Controls
Imports Windows.Media.Miracast
Imports Windows.System

Namespace Frames

    Public NotInheritable Class CastReceiverFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

            ConnectionsListView.ItemsSource = Connections

            LoadMiracast()

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
                CurrentSession.MaxSimultaneousConnections = 10
                AddHandler CurrentSession.ConnectionCreated, Sub(session As MiracastReceiverSession, args As MiracastReceiverConnectionCreatedEventArgs)
                                                                 Connections.Add(args.Connection)
                                                                 ConnectionPins.Add(args.Pin)
                                                             End Sub
                AddHandler CurrentSession.MediaSourceCreated, Sub(session As MiracastReceiverSession, args As MiracastReceiverMediaSourceCreatedEventArgs)
                                                                  Dim window = CType(App.Current, App).ApplicationManager.CreateNewWindow()
                                                                  window.Content = New MiraCastConnectionPage(window, args.Connection, args.MediaSource)
                                                                  window.IsCloseButtonEnabled = False
                                                                  window.Show()
                                                              End Sub
                AddHandler CurrentSession.Disconnected, Sub(session As MiracastReceiverSession, args As MiracastReceiverDisconnectedEventArgs)

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

        Public Sub TryRemoveConnection(connection As MiracastReceiverConnection)
            Dim item = Connections.Where(Function(x) x.Transmitter.MacAddress = connection.Transmitter.MacAddress).FirstOrDefault()
            If item IsNot Nothing Then
                ConnectionPins.RemoveAt(Connections.IndexOf(item))
                Connections.Remove(item)
            End If
        End Sub

#End Region

#Region "Links"

        Private Sub MiracastToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:project"))
        End Sub

#End Region
    End Class

End Namespace