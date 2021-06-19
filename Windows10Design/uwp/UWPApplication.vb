#If UAP10_0 Then

Imports Microsoft.Toolkit.Win32.UI.XamlHost

Public Class UWPApplication
    Inherits XamlApplication

    Public ReadOnly Property ApplicationManager As IApplicationCore

    Public Sub New(applicationManager As IApplicationCore)

        Initialize()
        Me.ApplicationManager = applicationManager

    End Sub

End Class


#End If