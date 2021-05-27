
Namespace Controls

    ' https://docs.microsoft.com/en-us/windows/uwp/launch-resume/launch-settings-app

    Public NotInheritable Class ToolPresenter
        Inherits UserControl

        Public Sub New()

            InitializeComponent()
            AddHandler MainButton.Click, Sub(sender As System.Object, e As RoutedEventArgs)
                                             RaiseEvent Click(sender, e)
                                         End Sub
        End Sub

        Public Event Click As RoutedEventHandler

        Public Property Title As String
            Get
                Return TitleTextBlock.Text
            End Get
            Set(value As String)
                TitleTextBlock.Text = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return DescriptionTextBlock.Text
            End Get
            Set(value As String)
                DescriptionTextBlock.Text = value
            End Set
        End Property

        Public Property Glyph As String
            Get
                Return FontIcon.Glyph
            End Get
            Set(value As String)
                FontIcon.Glyph = value
            End Set
        End Property

    End Class

End Namespace