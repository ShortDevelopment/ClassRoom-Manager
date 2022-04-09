Imports System.Threading
Imports FullTrustUWP.Core.Xaml

Public NotInheritable Class App

    Public Sub New()
        ' InitializeComponent()
    End Sub

    Public Sub CreateNewWindow(config As XamlWindowConfig, contentCallback As Func(Of UIElement))
        Dim thread As New Thread(Sub()
                                     Dim window = XamlWindowActivator.CreateNewWindow(config)
                                     window.Content = contentCallback()
                                     window.CoreWindow.Dispatcher.ProcessEvents(Windows.UI.Core.CoreProcessEventsOption.ProcessUntilQuit)
                                 End Sub)
        thread.IsBackground = True
        thread.SetApartmentState(ApartmentState.STA)
        thread.Start()
    End Sub

End Class
