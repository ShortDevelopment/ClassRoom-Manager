Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Class UserProfile

    Public ReadOnly Property UserName As String

    Public ReadOnly Property Avatar As Image
        Get
            Dim path = $"{IO.Path.GetPathRoot(Environment.SystemDirectory)}Users\{UserName}\AppData\Local\Temp\{UserName}.bmp"
            If Not File.Exists(path) Then
                Return DefaultAvatar
            End If
            Return Image.FromFile(path)
        End Get
    End Property

    Private Shared _DefaultAvatar As Image = Nothing
    Public Shared ReadOnly Property DefaultAvatar As Image
        Get
            If Not _DefaultAvatar Is Nothing Then Return _DefaultAvatar
            Dim path = $"{IO.Path.GetPathRoot(Environment.SystemDirectory)}ProgramData\Microsoft\User Account Pictures\User.bmp"
            If Not File.Exists(path) Then
                Return Nothing
            End If
            Return Image.FromFile(path)
        End Get
    End Property

    Public Sub New(UserName As String)
        Me._UserName = UserName
    End Sub

    Public Shared ReadOnly Property CurrentUser As UserProfile
        Get
            Return New UserProfile(Environment.UserName)
        End Get
    End Property

End Class