Namespace Frames

    Public NotInheritable Class ToolsFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

        End Sub
        Private Sub CoverToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Dim window = CType(App.Current, App).ApplicationManager.CreateNewWindow()
            window.Content = New CoverScreenPage(window)
            window.HasSystemTitleBar = False
            window.Show()
        End Sub

        Private Sub ChalkBoardToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Dim window = CType(App.Current, App).ApplicationManager.CreateNewWindow()
            window.Content = New ChalkBoardPage()
            window.Title = "Tafel"
            window.Show()
        End Sub

    End Class

End Namespace