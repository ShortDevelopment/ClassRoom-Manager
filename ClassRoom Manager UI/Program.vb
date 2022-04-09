Imports FullTrustUWP.Core.Xaml

Public Class Program

    <STAThread>
    Public Shared Sub Main(args As String())

        Using appWrapper As New XamlApplicationWrapper(Function() New App())

            Dim windowTitle = Process.GetCurrentProcess().ProcessName
            Dim window = XamlWindowActivator.CreateNewWindow(New XamlWindowConfig(windowTitle))
            window.Content = New MainPage()

            window.Dispatcher.ProcessEvents(Windows.UI.Core.CoreProcessEventsOption.ProcessUntilQuit)
        End Using
    End Sub

End Class
