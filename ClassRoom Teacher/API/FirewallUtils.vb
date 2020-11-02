Imports Microsoft.TeamFoundation.Common

' https://stackoverflow.com/a/41969335

Module FirewallUtils
    Function IsPortOpen(ByVal port As Integer) As Boolean
        EnsureSetup()
        Dim progID As Type = Type.GetTypeFromProgID("HNetCfg.FwMgr")
        Dim firewall As INetFwMgr = TryCast(Activator.CreateInstance(progID), INetFwMgr)
        Dim ports As INetFwOpenPorts = firewall.LocalPolicy.CurrentProfile.GloballyOpenPorts
        Dim portEnumerate As IEnumerator = ports.GetEnumerator()

        While (portEnumerate.MoveNext())
            Dim currentPort As INetFwOpenPort = TryCast(portEnumerate.Current, INetFwOpenPort)
            If currentPort.Port = port Then Return True
        End While

        Return False
    End Function

    Sub OpenPort(ByVal port As Integer, ByVal Name As String)
        EnsureSetup()
        If IsPortOpen(port) Then Return
        Dim openPort As INetFwOpenPort = TryCast(GetInstance("INetOpenPort"), INetFwOpenPort)
        openPort.Port = port
        openPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP
        openPort.Name = Name
        Dim openPorts As INetFwOpenPorts = sm_fwProfile.GloballyOpenPorts
        openPorts.Add(openPort)
    End Sub

    Sub ClosePort(ByVal port As Integer)
        EnsureSetup()
        If Not IsPortOpen(port) Then Return
        Dim ports As INetFwOpenPorts = sm_fwProfile.GloballyOpenPorts
        ports.Remove(port, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP)
    End Sub

    Private Function GetInstance(ByVal typeName As String) As Object
        Dim tpResult As Type = Nothing

        Select Case typeName
            Case "INetFwMgr"
                tpResult = Type.GetTypeFromCLSID(New Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"))
                Return Activator.CreateInstance(tpResult)
            Case "INetAuthApp"
                tpResult = Type.GetTypeFromCLSID(New Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"))
                Return Activator.CreateInstance(tpResult)
            Case "INetOpenPort"
                tpResult = Type.GetTypeFromCLSID(New Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"))
                Return Activator.CreateInstance(tpResult)
            Case Else
                Throw New Exception("Unknown type name: " & typeName)
        End Select
    End Function

    Private Sub EnsureSetup()
        If sm_fwProfile IsNot Nothing Then Return
        Dim fwMgr As INetFwMgr = TryCast(GetInstance("INetFwMgr"), INetFwMgr)
        sm_fwProfile = fwMgr.LocalPolicy.CurrentProfile
    End Sub

    Private sm_fwProfile As INetFwProfile = Nothing
End Module
