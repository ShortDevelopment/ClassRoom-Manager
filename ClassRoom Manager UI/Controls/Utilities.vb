Namespace Controls
    Module Utilities
        <Extension>
        Public Async Sub ShowErrorDialog(page As Page, ex As Exception)
            Try
                Dim dialog As New ErrorDialog(ex)
                dialog.XamlRoot = page.XamlRoot
                Await dialog.ShowAsync()
            Catch : End Try
        End Sub
        <Extension>
        Public Async Sub ShowErrorDialog(page As Page, title As String, msg As String)
            Try
                Dim dialog As New ErrorDialog(title, msg)
                dialog.XamlRoot = page.XamlRoot
                Await dialog.ShowAsync()
            Catch : End Try
        End Sub
    End Module

End Namespace