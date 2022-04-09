Imports FullTrustUWP.Core.Xaml

Namespace Frames

    Public NotInheritable Class ToolsFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

        End Sub
        Private Sub CoverToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            'window.HasSystemTitleBar = False
            'window.IsTopMost = True
            CType(App.Current, App).CreateNewWindow(
                New XamlWindowConfig("Mira Cast"),
                Function() New CoverScreenPage()
            )
        End Sub

        Private Sub ChalkBoardToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            CType(App.Current, App).CreateNewWindow(
                New XamlWindowConfig("Tafel"),
                Function() New ChalkBoardPage()
            )
        End Sub

    End Class

End Namespace