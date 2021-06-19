
Public Interface IApplicationCore

    Function CreateNewWindow() As IApplicationCoreWindow

End Interface

Public Interface IApplicationCoreWindow
    Sub Activate()
    Sub Show()
    Sub Hide()
    Sub Close()

    Property Title As String
    Property WindowState As CoreWindowState
    Property HasSystemTitleBar As Boolean
    Property IsFullScreen As Boolean
    Property IsTopMost As Boolean
    Property IsCloseButtonEnabled As Boolean

    Property Content As Object 'UIElement

End Interface

Public Enum CoreWindowState
    Normal = 0
    Minimized = 1
    Maximized = 2
End Enum