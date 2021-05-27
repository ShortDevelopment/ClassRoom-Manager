Imports ClassRoom_Manager.UI.Interop.Application
Imports Microsoft.Toolkit.Forms.UI.XamlHost
Imports Windows.UI.Xaml

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

#End Region

#End Region

End Class
