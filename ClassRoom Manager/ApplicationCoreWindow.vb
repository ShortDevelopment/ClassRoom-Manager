Imports ClassRoom_Manager.UI.Interop.Application
Imports Microsoft.Toolkit.Forms.UI.XamlHost
Imports Windows.UI.Xaml
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Threading

Public Class ApplicationCoreWindow
    Inherits Form
    Implements IApplicationCoreWindow

    Public Sub New()
        InitializeComponent()
        Title = System.Windows.Forms.Application.ProductName
    End Sub

    Public Sub New(type As Type)
        Me.New()
        Content = Activator.CreateInstance(type)
    End Sub

#Region "Hosting"

    Public ReadOnly Property XamlIsland As WindowsXamlHost

    Protected Sub InitializeComponent()
        _XamlIsland = New WindowsXamlHost()
        XamlIsland.Dock = DockStyle.Fill
        Me.Controls.Add(XamlIsland)
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

    Public Property Content As UIElement Implements IApplicationCoreWindow.Content
        Get
            Return XamlIsland.Child
        End Get
        Set(value As UIElement)
            XamlIsland.Child = value
        End Set
    End Property

#End Region

#Region "Behavior"
    Private Sub IApplicationCoreWindow_Activate() Implements IApplicationCoreWindow.Activate
        Me.BringToFront()
    End Sub

    Private Sub IApplicationCoreWindow_Show() Implements IApplicationCoreWindow.Show
        Me.Show()
    End Sub

    Private Sub IApplicationCoreWindow_Hide() Implements IApplicationCoreWindow.Hide
        Me.Hide()
    End Sub

    Private Sub IApplicationCoreWindow_Close() Implements IApplicationCoreWindow.Close
        Me.Close()
    End Sub

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

    Public Property HasSystemTitleBar As Boolean = True Implements IApplicationCoreWindow.HasSystemTitleBar

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
        If Not HasSystemTitleBar AndAlso Not m.WParam = IntPtr.Zero Then
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

#End Region

End Class
