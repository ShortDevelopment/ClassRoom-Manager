Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading

Module Extensions

    <Extension>
    Public Function RemoveInvalidFilePathCharacters(ByRef filename As String, Optional replaceChar As String = "_") As String
        Dim regexSearch As String = New String(Path.GetInvalidFileNameChars()) & New String(Path.GetInvalidPathChars())
        Dim r As Regex = New Regex(String.Format("[{0}]", Regex.Escape(regexSearch)))
        Return r.Replace(filename, replaceChar)
    End Function

    Public Function RunOnNewThread(code As Action) As Thread
        Dim t As New Thread(Sub()
                                Try
                                    code?.Invoke()
                                Catch ex As Exception

                                End Try
                            End Sub)
        t.IsBackground = True
        t.Start()
        Return t
    End Function

    <DllImport("user32.dll")>
    Private Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean : End Function

    <Extension>
    Public Sub Focus(ByRef form As Form)
        SetForegroundWindow(form.Handle)
    End Sub

End Module
