Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading.Tasks
Public Class NetworkManager
    Public Const HiQnetPort As Integer = 3804

    Public Shared Function GetBroadcastAddress(ByVal ipAddress As String, ByVal subnetMask As String) As String
        Dim ip = System.Net.IPAddress.Parse(ipAddress)
        Dim mask = Net.IPAddress.Parse(subnetMask)
        Dim ipAdressBytes As Byte() = ip.GetAddressBytes()
        Dim subnetMaskBytes As Byte() = mask.GetAddressBytes()
        If ipAdressBytes.Length <> subnetMaskBytes.Length Then Throw New ArgumentException("Lengths of IP address and subnet mask do not match.")
        Dim broadcastAddress As Byte() = New Byte(ipAdressBytes.Length - 1) {}

        For i As Integer = 0 To broadcastAddress.Length - 1
            broadcastAddress(i) = CByte((ipAdressBytes(i) Or (subnetMaskBytes(i) Xor 255)))
        Next

        Return New IPAddress(broadcastAddress).ToString()
    End Function

    Public Sub New(ByVal adapter As NetworkInterface)
        Me.NetworkAdapter = adapter
    End Sub

    Public Property NetworkAdapter As NetworkInterface

    Public Function GetIPAddress() As IPAddress
        For Each unicastIPAddressInformation As UnicastIPAddressInformation In NetworkAdapter.GetIPProperties().UnicastAddresses

            If unicastIPAddressInformation.Address.AddressFamily = AddressFamily.InterNetwork Then
                Return unicastIPAddressInformation.Address
            End If
        Next

        Throw New ArgumentException("Can't find IP")
    End Function

    Public Function GetSubnetMask() As IPAddress
        For Each unicastIPAddressInformation As UnicastIPAddressInformation In NetworkAdapter.GetIPProperties().UnicastAddresses

            If unicastIPAddressInformation.Address.AddressFamily = AddressFamily.InterNetwork Then
                Return unicastIPAddressInformation.IPv4Mask
            End If
        Next

        Throw New ArgumentException("Can't find subnetmask")
    End Function

    Public Function GetMacAddress() As PhysicalAddress
        Return NetworkAdapter.GetPhysicalAddress()
    End Function

    Public Function GetGateway() As IPAddress
        For Each IPAddressInformation As GatewayIPAddressInformation In NetworkAdapter.GetIPProperties().GatewayAddresses
            Return IPAddressInformation.Address

            If IPAddressInformation.Address.AddressFamily = AddressFamily.InterNetwork Then
                Return IPAddressInformation.Address
            End If
        Next

        Throw New ArgumentException("Can't find subnetmask")
    End Function

    Public Shared ReadOnly Property DefaultWiFi As NetworkManager
        Get

            For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()

                If ni.OperationalStatus = OperationalStatus.Up AndAlso ni.NetworkInterfaceType = NetworkInterfaceType.Wireless80211 Then
                    Return New NetworkManager(ni)
                End If
            Next

            Throw New FileNotFoundException()
        End Get
    End Property

    Public Shared ReadOnly Property DefaultEthernet As NetworkManager
        Get

            For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()

                If ni.OperationalStatus = OperationalStatus.Up AndAlso ni.NetworkInterfaceType = NetworkInterfaceType.Ethernet Then
                    Return New NetworkManager(ni)
                End If
            Next

            Throw New FileNotFoundException()
        End Get
    End Property

    Public Shared ReadOnly Property [Default] As NetworkManager
        Get

            For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()

                If Not ni.Description.ToLower().Contains("virtual") AndAlso ni.OperationalStatus = OperationalStatus.Up AndAlso (ni.NetworkInterfaceType = NetworkInterfaceType.Wireless80211 OrElse ni.NetworkInterfaceType = NetworkInterfaceType.Ethernet) Then
                    Debug.Print(ni.Id)
                    Debug.Print(ni.Description)
                    Return New NetworkManager(ni)
                End If
            Next

            Throw New FileNotFoundException()
        End Get
    End Property

    Public Shared Function FromIP(ByVal ip As IPAddress) As NetworkManager
        For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()

            For Each unicastIPAddressInformation As UnicastIPAddressInformation In ni.GetIPProperties().UnicastAddresses

                If unicastIPAddressInformation.Address.AddressFamily = AddressFamily.InterNetwork Then
                    If unicastIPAddressInformation.Address.Equals(ip) Then Return New NetworkManager(ni)
                End If
            Next
        Next

        Throw New ArgumentException("Can't find IP")
    End Function
End Class
