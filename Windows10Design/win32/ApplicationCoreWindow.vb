#If NETCOREAPP3_1 Then

Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Threading
Imports FormsApplication = System.Windows.Forms.Application
Imports System.Windows.Forms
Imports Microsoft.Toolkit.Forms.UI.XamlHost
Imports System.Drawing

Public Class ApplicationCoreWindow
    Inherits Form
    Implements IApplicationCoreWindow

    Public Sub New()
        Title = FormsApplication.ProductName
    End Sub

#Region "SplashScreen"
    Public Property IsSplashScreenVisible As Boolean
    Public ReadOnly Property SplashScreenColor As Color = Color.LightGray

    Public Sub ShowSplashScreen()
        IsSplashScreenVisible = True
        Me.Refresh()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If IsSplashScreenVisible Then

            Using g As Graphics = CreateGraphics()
                g.Clear(SplashScreenColor)
                Const IconWidth As Integer = 100
                g.DrawIcon(Me.Icon, New Rectangle(Me.Width / 2 - IconWidth / 2, Me.Height / 2 - IconWidth / 2, IconWidth, IconWidth))
            End Using
        Else
            MyBase.OnPaint(e)
        End If
    End Sub
#End Region

#Region "Hosting"

    Public ReadOnly Property XamlIsland As WindowsXamlHost

    Public Sub LoadXamlContent()
        IsSplashScreenVisible = False
        Me.Refresh()

        If Not DesignMode Then
            _XamlIsland = New WindowsXamlHost()
            XamlIsland.Dock = DockStyle.Fill
            Me.Controls.Add(XamlIsland)
        End If
    End Sub

    Protected Overrides Sub OnClosing(e As CancelEventArgs)
        Dim obj = XamlIsland.GetUwpInternalObject()
        If Not obj Is Nothing Then
            e.Cancel = True
            XamlIsland.Child = Nothing

            ' Prevent application terminating
            Dim t As New Thread(Sub() Me.Invoke(Sub() Close()))
            t.IsBackground = True
            t.Start()
        End If
        MyBase.OnClosing(e)
    End Sub

#End Region

#Region "IApplicationCoreWindow"

#Region "Xaml Islands"

    Public Property Content As Object Implements IApplicationCoreWindow.Content
        Get
            Return XamlIsland.Child
        End Get
        Set(value As Object)
            XamlIsland.Child = value
        End Set
    End Property

#End Region

#Region "Behavior"
    Private Sub IApplicationCoreWindow_Activate() Implements IApplicationCoreWindow.Activate
        MyBase.BringToFront()
    End Sub

    Private Sub IApplicationCoreWindow_Show() Implements IApplicationCoreWindow.Show
        MyBase.Show()
    End Sub

    Private Sub IApplicationCoreWindow_Hide() Implements IApplicationCoreWindow.Hide
        MyBase.Hide()
    End Sub

    Private Sub IApplicationCoreWindow_Close() Implements IApplicationCoreWindow.Close
        MyBase.Close()
    End Sub

    Public Property IsTopMost As Boolean Implements IApplicationCoreWindow.IsTopMost
        Get
            Return MyBase.TopMost
        End Get
        Set(value As Boolean)
            MyBase.TopMost = value
        End Set
    End Property

#End Region

#Region "UI"

    Public Property Title As String Implements IApplicationCoreWindow.Title
        Get
            Return Me.Text
        End Get
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    Private Property IApplicationCoreWindow_WindowState As CoreWindowState Implements IApplicationCoreWindow.WindowState
        Get
            Return CType(Me.WindowState, CoreWindowState)
        End Get
        Set(value As CoreWindowState)
            Me.WindowState = CType(value, FormWindowState)
        End Set
    End Property

#Region "Hide System Title Bar"

    Dim _HasSystemTitleBar As Boolean = True
    Public Property HasSystemTitleBar As Boolean Implements IApplicationCoreWindow.HasSystemTitleBar
        Get
            Return _HasSystemTitleBar
        End Get
        Set(value As Boolean)
            _HasSystemTitleBar = value
            If value Then
                Me.Controls.Remove(InputCaptureWindow)
            Else
                If InputCaptureWindow Is Nothing Then InitializeInputCaptureWindow()
                Me.Controls.Add(InputCaptureWindow)
                InputCaptureWindow.BringToFront()
            End If
            FormsApplication.DoEvents()
        End Set
    End Property

#End Region

#Region "FullScreen"

    Dim _IsFullScreen As Boolean = False
    Public Property IsFullScreen As Boolean Implements IApplicationCoreWindow.IsFullScreen
        Get
            Return _IsFullScreen
        End Get
        Set(value As Boolean)
            If value Then
                WindowState = FormWindowState.Normal
                FormBorderStyle = FormBorderStyle.None
                WindowState = FormWindowState.Maximized
            Else
                WindowState = FormWindowState.Normal
                FormBorderStyle = FormBorderStyle.Sizable
            End If
            _IsFullScreen = value
        End Set
    End Property

#End Region

#Region "Drag Bar"
    Public ReadOnly Property InputCaptureWindow As InputCaptureWindow
    Private Sub InitializeInputCaptureWindow()
        Me._InputCaptureWindow = New InputCaptureWindow()
        Me.InputCaptureWindow.Dock = DockStyle.Top
        Me.InputCaptureWindow.Height = 80
        Me.Visible = True
        AddHandler Me.InputCaptureWindow.MouseDown, Sub()
                                                        Me.StartWindowMoving()
                                                    End Sub
    End Sub

#End Region

#Region "Close Button"
    ' https://devblogs.microsoft.com/oldnewthing/20100604-00/?p=13803
    <DllImport("user32.dll")>
    Private Shared Function EnableMenuItem(ByVal hMenu As IntPtr, ByVal uIDEnableItem As UInteger, ByVal uEnable As UInteger) As Boolean : End Function

    <DllImport("user32.dll")>
    Private Shared Function GetSystemMenu(ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As IntPtr : End Function

    Dim _IsCloseButtonEnabled As Boolean = True
    Public Property IsCloseButtonEnabled As Boolean Implements IApplicationCoreWindow.IsCloseButtonEnabled
        Get
            Return _IsCloseButtonEnabled
        End Get
        Set(value As Boolean)
            _IsCloseButtonEnabled = value
            If value Then
                EnableMenuItem(GetSystemMenu(Handle, False), InternalWin32MenuConstants.SC_CLOSE, InternalWin32MenuConstants.MF_BYCOMMAND Or InternalWin32MenuConstants.MF_ENABLED)
            Else
                EnableMenuItem(GetSystemMenu(Handle, False), InternalWin32MenuConstants.SC_CLOSE, InternalWin32MenuConstants.MF_BYCOMMAND Or InternalWin32MenuConstants.MF_DISABLED Or InternalWin32MenuConstants.MF_GRAYED)
            End If
        End Set
    End Property

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        ' Force setting style again
        IsCloseButtonEnabled = IsCloseButtonEnabled
    End Sub

    Protected Class InternalWin32MenuConstants
        Friend Const SC_CLOSE As Integer = &HF060

        Friend Const MF_INSERT As UInt32 = &H0
        Friend Const MF_CHANGE As UInt32 = &H80
        Friend Const MF_APPEND As UInt32 = &H100
        Friend Const MF_DELETE As UInt32 = &H200
        Friend Const MF_REMOVE As UInt32 = &H1000
        Friend Const MF_BYCOMMAND As UInt32 = &H0
        Friend Const MF_BYPOSITION As UInt32 = &H400
        Friend Const MF_SEPARATOR As UInt32 = &H800
        Friend Const MF_ENABLED As UInt32 = &H0
        Friend Const MF_GRAYED As UInt32 = &H1
        Friend Const MF_DISABLED As UInt32 = &H2
        Friend Const MF_UNCHECKED As UInt32 = &H0
        Friend Const MF_CHECKED As UInt32 = &H8
        Friend Const MF_USECHECKBITMAPS As UInt32 = &H200
        Friend Const MF_STRING As UInt32 = &H0
        Friend Const MF_BITMAP As UInt32 = &H4
        Friend Const MF_OWNERDRAW As UInt32 = &H100
        Friend Const MF_POPUP As UInt32 = &H10
        Friend Const MF_MENUBARBREAK As UInt32 = &H20
        Friend Const MF_MENUBREAK As UInt32 = &H40
        Friend Const MF_UNHILITE As UInt32 = &H0
        Friend Const MF_HILITE As UInt32 = &H80
        Friend Const MF_DEFAULT As UInt32 = &H1000
        Friend Const MF_SYSMENU As UInt32 = &H2000
        Friend Const MF_HELP As UInt32 = &H4000
        Friend Const MF_RIGHTJUSTIFY As UInt32 = &H4000
        Friend Const MF_MOUSESELECT As UInt32 = &H8000
        Friend Const MF_END As UInt32 = &H80
        Friend Const MFT_STRING As UInt32 = MF_STRING
        Friend Const MFT_BITMAP As UInt32 = MF_BITMAP
        Friend Const MFT_MENUBARBREAK As UInt32 = MF_MENUBARBREAK
        Friend Const MFT_MENUBREAK As UInt32 = MF_MENUBREAK
        Friend Const MFT_OWNERDRAW As UInt32 = MF_OWNERDRAW
        Friend Const MFT_RADIOCHECK As UInt32 = &H200
        Friend Const MFT_SEPARATOR As UInt32 = MF_SEPARATOR
        Friend Const MFT_RIGHTORDER As UInt32 = &H2000
        Friend Const MFT_RIGHTJUSTIFY As UInt32 = MF_RIGHTJUSTIFY
        Friend Const MFS_GRAYED As UInt32 = &H3
        Friend Const MFS_DISABLED As UInt32 = MFS_GRAYED
        Friend Const MFS_CHECKED As UInt32 = MF_CHECKED
        Friend Const MFS_HILITE As UInt32 = MF_HILITE
        Friend Const MFS_ENABLED As UInt32 = MF_ENABLED
        Friend Const MFS_UNCHECKED As UInt32 = MF_UNCHECKED
        Friend Const MFS_UNHILITE As UInt32 = MF_UNHILITE
        Friend Const MFS_DEFAULT As UInt32 = MF_DEFAULT
    End Class
#End Region

#End Region

#End Region

#Region "Window Native"

#Region "WndProc"

    Protected Overrides Sub WndProc(ByRef m As Message)
        If DesignMode Then MyBase.WndProc(m)

#Region "Constatnts"
        Const WM_NCCALCSIZE As Integer = &H83
#End Region

        Select Case m.Msg
            Case WM_NCCALCSIZE
                WmNCCalcSize(m)
            Case Else
                MyBase.WndProc(m)
        End Select

    End Sub

#Region "WM_NCCALCSIZE"

    ''' <summary>
    ''' https//github.com/microsoft/terminal/blob/ff8fdbd2431f1cfd8211833815be481dfdec4420/src/cascadia/WindowsTerminal/NonClientIslandWindow.cpp#L405
    ''' </summary>
    Public Sub WmNCCalcSize(m)
        If Not HasSystemTitleBar AndAlso Not IsFullScreen AndAlso Not m.WParam = IntPtr.Zero Then
            Dim topOld = Marshal.PtrToStructure(Of NCCALCSIZE_PARAMS)(m.LParam).rgrc0.top

            ' Run default processing
            DefWndProc(m)

            Dim nccsp = Marshal.PtrToStructure(Of NCCALCSIZE_PARAMS)(m.LParam)
            ' Rest to old top (remove title bar)
            nccsp.rgrc0.top = topOld
            Marshal.StructureToPtr(nccsp, m.LParam, True)
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    <StructLayout(LayoutKind.Sequential)>
    Protected Structure NCCALCSIZE_PARAMS
        Public rgrc0, rgrc1, rgrc2 As RECT
        Public lppos As WINDOWPOS
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Protected Structure RECT
        Public left, top, right, bottom As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Protected Structure WINDOWPOS
        Public hwnd As IntPtr
        Public hwndinsertafter As IntPtr
        Public x, y, cx, cy As Integer
        Public flags As Integer
    End Structure

#End Region

#End Region

    Const WS_EX_NOREDIRECTIONBITMAP As Integer = &H200000L
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim params = MyBase.CreateParams
            ' params.ExStyle = params.ExStyle Or WS_EX_NOREDIRECTIONBITMAP // Disabled for SplashScreen drawing
            Return params
        End Get
    End Property

#Region "Moving Window"
    <DllImport("user32.dll")>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer : End Function
    <DllImport("user32.dll")>
    Private Shared Function ReleaseCapture() As Boolean : End Function
    <DllImport("user32.dll")>
    Private Shared Function SetCapture(ByVal hWnd As IntPtr) As IntPtr : End Function

    Public Sub StartWindowMoving()
        Const WM_NCLBUTTONDOWN As Integer = &HA1
        Const HT_CAPTION As Integer = &H2
        ReleaseCapture()
        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
    End Sub
#End Region

#End Region

End Class

#End If