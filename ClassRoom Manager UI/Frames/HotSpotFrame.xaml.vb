
Imports ClassRoom_Manager.UI.Interop
Imports Windows.Networking.Connectivity
Imports Windows.Networking.NetworkOperators
Imports Windows.System

Namespace Frames

    Public NotInheritable Class HotSpotFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()

            LoadHotSpot()
            LoadProxy()

        End Sub

#Region "HotSpot"
        Public ReadOnly Property ConnectionProfile As ConnectionProfile
        Public ReadOnly Property HotSpotManager As NetworkOperatorTetheringManager 'NetworkOperatorTetheringAccessPointConfiguration
        Public Async Sub LoadHotSpot()
            Try
                _ConnectionProfile = NetworkInformation.GetInternetConnectionProfile()
            Catch : End Try
            If ConnectionProfile Is Nothing Then _ConnectionProfile = NetworkInformation.GetConnectionProfiles()(0)

            _HotSpotManager = NetworkOperatorTetheringManager.CreateFromConnectionProfile(ConnectionProfile)

            ShowHotSpotInfo()
        End Sub

        Dim settingHotSpotEnabledToggleSwitch_Toggled_Internal As Boolean = False
        Private Async Sub HotSpotEnabledToggleSwitch_Toggled(sender As Object, e As RoutedEventArgs)
            If settingHotSpotEnabledToggleSwitch_Toggled_Internal Then
                settingHotSpotEnabledToggleSwitch_Toggled_Internal = False
                Exit Sub
            End If

            Dim status As NetworkOperatorTetheringOperationResult
            If HotSpotEnabledToggleSwitch.IsOn Then
                status = Await HotSpotManager.StartTetheringAsync()

            Else
                status = Await HotSpotManager.StopTetheringAsync()
            End If

            ShowHotSpotInfo()
        End Sub

        Public Sub ShowHotSpotInfo()
            settingHotSpotEnabledToggleSwitch_Toggled_Internal = True
            HotSpotEnabledToggleSwitch.IsOn = HotSpotManager.TetheringOperationalState = TetheringOperationalState.On

            Dim config = HotSpotManager.GetCurrentAccessPointConfiguration()
            HotSpotNameTextBox.Text = config.Ssid
            HotSpotPasswordBox.Password = config.Passphrase
            HotSpotPasswordBox.IsPasswordRevealButtonEnabled = True
        End Sub

        Private Async Sub ApplyHotSpotChangesButton_Click(sender As Object, e As RoutedEventArgs)
            Try
                Dim config = HotSpotManager.GetCurrentAccessPointConfiguration()
                config.Ssid = HotSpotNameTextBox.Text
                config.Passphrase = HotSpotPasswordBox.Password
                Await HotSpotManager.ConfigureAccessPointAsync(config)
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "Proxy"

        Public Async Sub LoadProxy()
            Dim proxyInfo = Net.WebRequest.GetSystemWebProxy()
            Dim url = "https://google.com/"
            Dim proxy = proxyInfo.GetProxy(New Uri(url))

            If proxy IsNot Nothing AndAlso Not proxy.AbsoluteUri = url Then

                ' Has Proxy
                ProxyURLTextBox.Text = New Uri(proxy.AbsoluteUri).Host
                ProxyPortTextBox.Text = proxy.Port.ToString()

            Else

                ' No Proxy
                ProxyURLTextBox.Text = "-"
                ProxyPortTextBox.Text = ""

            End If

        End Sub

#End Region

#Region "Links"
        Private Sub HotSpotToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:network-mobilehotspot"))
        End Sub

        Private Sub WLanToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:network-wifi"))
        End Sub

        Private Sub LanToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:network-ethernet"))
        End Sub

#End Region
    End Class

End Namespace