Imports ClassRoom_Manager.UI.Interop.Application
Imports Microsoft.Toolkit.Win32.UI.XamlHost

Public NotInheritable Class App
    Inherits XamlApplication

    Public ReadOnly Property ApplicationManager As IApplicationCore

    Public Sub New(applicationManager As IApplicationCore)

        Initialize()
        Me.ApplicationManager = applicationManager

        'Dim data = Resources.ToArray()
        'Dim x = Resources("ApplicationPageBackgroundThemeBrush")

        'Resources("ApplicationPageBackgroundThemeBrush") = Windows.UI.Colors.Red

    End Sub

    Public Sub UnhandledExeption(sender As System.Object, e As Windows.UI.Xaml.UnhandledExceptionEventArgs) Handles Me.UnhandledException
        e.Handled = True
        If Debugger.IsAttached Then Debugger.Break()
    End Sub

End Class
