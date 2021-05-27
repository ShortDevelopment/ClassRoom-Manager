Namespace Controls
    Public NotInheritable Class ErrorDialog
        Inherits ContentDialog

        Public Sub New(ex As Exception)
            Me.New(ex.GetType().Name, ex.Message)
        End Sub

        Public Sub New(title As String, msg As String)
            InitializeComponent()
            Me.Title = $"Fehler - {title}"
            Me.Content = msg
        End Sub

        Private Sub OKButton_Click(sender As ContentDialog, args As ContentDialogButtonClickEventArgs) : End Sub

    End Class

End Namespace