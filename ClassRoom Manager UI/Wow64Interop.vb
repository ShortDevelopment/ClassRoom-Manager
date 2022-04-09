Imports System.Runtime.InteropServices

Namespace Interop

    ''' <summary>
    ''' https://ourcodeworld.com/articles/read/909/how-to-run-any-executable-inside-the-system32-directory-of-windows-with-c-sharp-in-winforms
    ''' </summary>
    Public Class Wow64Interop

        Public Shared Sub RunWithoutRedirect(action As Action)
            Dim wow64Value As IntPtr = IntPtr.Zero
            DisableFSRedirection(wow64Value)
            action?.Invoke()
            RevertFsRedirection(wow64Value)
        End Sub

        <DllImport("kernel32", EntryPoint:="Wow64DisableWow64FsRedirection")>
        Public Shared Function DisableFSRedirection(ByRef ptr As IntPtr) As Boolean : End Function

        <DllImport("kernel32", EntryPoint:="Wow64RevertWow64FsRedirection")>
        Public Shared Function RevertFsRedirection(ByVal ptr As IntPtr) As Boolean : End Function

    End Class

End Namespace