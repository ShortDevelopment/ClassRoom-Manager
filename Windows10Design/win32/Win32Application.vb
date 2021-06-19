#If NETCOREAPP3_1 Then

Imports System.Collections.ObjectModel
Imports System.Windows.Forms

Public MustInherit Class Win32Application
    Implements IApplicationCore

    Public ReadOnly Property CommandLineArgs As ReadOnlyCollection(Of String)

    Public Sub New(args As String())
        CommandLineArgs = New ReadOnlyCollection(Of String)(args)

        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
    End Sub

    Public Sub Run(window As ApplicationCoreWindow)

        AddHandler AppDomain.CurrentDomain.UnhandledException, Sub(sender As Object, e As UnhandledExceptionEventArgs)
                                                                   Dim ex As Exception = e.ExceptionObject
                                                                   If Debugger.IsAttached Then Debugger.Break()
                                                               End Sub

        window.Show()
        window.ShowSplashScreen()

        Using CreateUWPApplication()
            window.LoadXamlContent()
            window.Content = GetXamlContent()
            Application.Run(window)
        End Using
    End Sub

    Public MustOverride Function CreateUWPApplication() As IDisposable 'UWPApplication
    Public MustOverride Function GetXamlContent() As Object

    Public Function CreateNewWindow() As IApplicationCoreWindow Implements IApplicationCore.CreateNewWindow
        Return New ApplicationCoreWindow()
    End Function

End Class

#End If