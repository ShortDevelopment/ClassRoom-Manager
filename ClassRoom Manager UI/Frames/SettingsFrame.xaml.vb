Namespace Frames

    Public NotInheritable Class SettingsFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

        End Sub

        Public ReadOnly Property CurrentYear As String = DateTime.Now.ToString("yyyy")

    End Class

End Namespace