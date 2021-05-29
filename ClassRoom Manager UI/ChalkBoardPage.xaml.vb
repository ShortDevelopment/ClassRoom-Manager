Imports Windows.UI.Core

Public NotInheritable Class ChalkBoardPage
    Inherits Page

    Public Sub New()

        InitializeComponent()

        MainInkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse Or CoreInputDeviceTypes.Pen Or CoreInputDeviceTypes.Touch

    End Sub

End Class
