Imports Windows.UI.Core
Imports Windows.UI.Input.Inking

Public NotInheritable Class ChalkBoardPage
    Inherits Page

    Public Sub New()

        InitializeComponent()

        MainInkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse Or CoreInputDeviceTypes.Pen Or CoreInputDeviceTypes.Touch

    End Sub

    Private Sub UndoButton_Click(sender As Object, e As RoutedEventArgs)
        Dim strokeContainer = MainInkCanvas.InkPresenter.StrokeContainer
        Dim stroke As InkStroke = strokeContainer.GetStrokes().LastOrDefault()

        If stroke IsNot Nothing Then
            stroke.Selected = True
            strokeContainer.DeleteSelected()
        End If
    End Sub
End Class
