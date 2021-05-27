Namespace Interop.Application
    Public Interface IApplicationCore

        Function CreateNewWindow() As IApplicationCoreWindow

    End Interface

    Public Interface IApplicationCoreWindow
        Inherits IDisposable

        Sub Activate()
        Sub Show()
        Sub Hide()

        Property Title As String
        Property WindowState As CoreWindowState

        Property Content As UIElement

    End Interface

    Public Enum CoreWindowState
        Normal = 0
        Minimized = 1
        Maximized = 2
    End Enum

End Namespace