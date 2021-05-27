Imports Microsoft.Toolkit.Win32.UI.XamlHost
''' <summary>
''' Stellt das anwendungsspezifische Verhalten bereit, um die Standardanwendungsklasse zu ergänzen.
''' </summary>
Public NotInheritable Class App
    Inherits XamlApplication

    Public Sub New()

        Initialize()
        'InitializeComponent()

    End Sub

    Public Sub UnhandledExeption(sender As System.Object, e As Windows.UI.Xaml.UnhandledExceptionEventArgs) Handles Me.UnhandledException
        e.Handled = True
        If Debugger.IsAttached Then Debugger.Break()
    End Sub

End Class
