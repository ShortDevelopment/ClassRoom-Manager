Public Class Program

    <STAThread>
    Public Shared Sub Main(args As String())

        Using New UI.App()
            Application.Run(New Form1())
        End Using

    End Sub

End Class
