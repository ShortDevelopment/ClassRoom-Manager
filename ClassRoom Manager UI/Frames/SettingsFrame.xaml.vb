Namespace Frames

    Public NotInheritable Class SettingsFrame
        Inherits Page

        Public ReadOnly Property CurrentYear As String = DateTime.Now.ToString("yyyy")

    End Class

End Namespace