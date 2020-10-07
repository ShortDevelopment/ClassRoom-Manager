Public Class RandomStringMaker

    Private Shared Alphabet As String = "abcdefghijklmnopqrstuvwxyz"

    Public Shared Function GetRandomString(CharacterCount As Integer) As String
        Dim out As String
        Dim rand As New Random(Date.Now.Millisecond)
        For i As Integer = 1 To CharacterCount
            out += Alphabet(rand.Next(0, Alphabet.Count - 1))
        Next
        Return out
    End Function

End Class
