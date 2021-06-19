#If NETCOREAPP3_1 Then

Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Windows10Design.Api

Namespace Api

    <Flags>
    Public Enum WindowStylesEx
        WS_EX_ACCEPTFILES = &H10
        WS_EX_APPWINDOW = &H40000
        WS_EX_CLIENTEDGE = &H200
        WS_EX_COMPOSITED = &H2000000
        WS_EX_CONTEXTHELP = &H400
        WS_EX_CONTROLPARENT = &H10000
        WS_EX_DLGMODALFRAME = &H1
        WS_EX_LAYERED = &H80000
        WS_EX_LAYOUTRTL = &H400000
        WS_EX_LEFT = &H0
        WS_EX_LEFTSCROLLBAR = &H4000
        WS_EX_LTRREADING = &H0
        WS_EX_MDICHILD = &H40
        WS_EX_NOACTIVATE = &H8000000
        WS_EX_NOINHERITLAYOUT = &H100000
        WS_EX_NOPARENTNOTIFY = &H4
        WS_EX_NOREDIRECTIONBITMAP = &H200000
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST
        WS_EX_RIGHT = &H1000
        WS_EX_RIGHTSCROLLBAR = &H0
        WS_EX_RTLREADING = &H2000
        WS_EX_STATICEDGE = &H20000
        WS_EX_TOOLWINDOW = &H80
        WS_EX_TOPMOST = &H8
        WS_EX_TRANSPARENT = &H20
        WS_EX_WINDOWEDGE = &H100
    End Enum

    <Flags>
    Public Enum WindowStyles As UInteger
        WS_BORDER = &H800000
        WS_CAPTION = &HC00000
        WS_CHILD = &H40000000
        WS_CLIPCHILDREN = &H2000000
        WS_CLIPSIBLINGS = &H4000000
        WS_DISABLED = &H8000000
        WS_DLGFRAME = &H400000
        WS_GROUP = &H20000
        WS_HSCROLL = &H100000
        WS_MAXIMIZE = &H1000000
        WS_MAXIMIZEBOX = &H10000
        WS_MINIMIZE = &H20000000
        WS_MINIMIZEBOX = &H20000
        WS_OVERLAPPED = &H0
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED Or WS_CAPTION Or WS_SYSMENU Or WS_SIZEFRAME Or WS_MINIMIZEBOX Or WS_MAXIMIZEBOX
        WS_POPUP = &H80000000UI
        WS_POPUPWINDOW = WS_POPUP Or WS_BORDER Or WS_SYSMENU
        WS_SIZEFRAME = &H40000
        WS_SYSMENU = &H80000
        WS_TABSTOP = &H10000
        WS_VISIBLE = &H10000000
        WS_VSCROLL = &H200000
    End Enum

End Namespace
Public Class InputCaptureWindow
    Inherits Panel

    Public Sub New()
        SetStyle(ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim x = MyBase.CreateParams
            x.Style = WindowStyles.WS_CHILD Or WindowStyles.WS_VISIBLE
            x.ExStyle = WindowStylesEx.WS_EX_LAYERED
            Return x
        End Get
    End Property

    Public Overloads Sub Show()
        If Parent Is Nothing Then Throw New ArgumentException("You have to set a Parent!")
        MyBase.Show()
        MyBase.BringToFront()
    End Sub

    Protected Overrides Sub OnResize(ByVal eventargs As EventArgs)
        Using bitmap = New Bitmap(Width, Height)

            Using g = Graphics.FromImage(bitmap)
                g.Clear(Color.FromArgb(1, Color.White))
            End Using

            SetBackground(bitmap)
        End Using
    End Sub

    Public Sub SetBackground(ByVal bitmap As Bitmap)
        Dim control = Me
        control.Width = bitmap.Width
        control.Height = bitmap.Height

        If bitmap.PixelFormat <> System.Drawing.Imaging.PixelFormat.Format32bppArgb Then
            Throw New ApplicationException("Wrong PixelFormat")
        End If

        Dim hBitmap As IntPtr = IntPtr.Zero
        Dim oldBitmap As IntPtr = IntPtr.Zero
        Dim screenDc As IntPtr = Win32.GetDC(IntPtr.Zero)
        Dim memDc As IntPtr = Win32.CreateCompatibleDC(screenDc)

        Try
            hBitmap = bitmap.GetHbitmap(Color.FromArgb(0))
            oldBitmap = Win32.SelectObject(memDc, hBitmap)
            Dim size As Win32.Size = New Win32.Size(bitmap.Width, bitmap.Height)
            Dim pointSource As Win32.Point = New Win32.Point(control.Left, control.Top)
            Dim topPos As Win32.Point = New Win32.Point(0, 0)
            Dim blend As Win32.BLENDFUNCTION = New Win32.BLENDFUNCTION()
            blend.BlendOp = 0
            blend.BlendFlags = 0
            blend.SourceConstantAlpha = Byte.MaxValue
            blend.AlphaFormat = 1
            Win32.UpdateLayeredWindow(control.Handle, screenDc, topPos, size, memDc, pointSource, 0, blend, 2)
        Catch ex As Exception
        Finally
            Win32.ReleaseDC(IntPtr.Zero, screenDc)

            If hBitmap <> IntPtr.Zero Then
                Win32.SelectObject(memDc, oldBitmap)
                Win32.DeleteObject(hBitmap)
            End If

            Win32.DeleteDC(memDc)
        End Try
    End Sub

    Protected Class Win32
        Public Enum Bool
            [False] = 0
            [True] = 1
        End Enum

        Public Structure Point
            Public x As Integer
            Public y As Integer

            Public Sub New(ByVal x As Integer, ByVal y As Integer)
                Me.x = x
                Me.y = y
            End Sub
        End Structure

        Public Structure Size
            Public cx As Integer
            Public cy As Integer

            Public Sub New(ByVal cx As Integer, ByVal cy As Integer)
                Me.cx = cx
                Me.cy = cy
            End Sub
        End Structure

        Public Structure BLENDFUNCTION
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
        End Structure

        Public Const ULW_ALPHA As Integer = 2
        Public Const AC_SRC_OVER As Byte = 0
        Public Const AC_SRC_ALPHA As Byte = 1
        <DllImport("user32.dll")>
        Public Shared Function UpdateLayeredWindow(ByVal handle As IntPtr, ByVal hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, ByVal hdcSrc As IntPtr, ByRef pprSrc As Point, ByVal crKey As Integer, ByRef pblend As BLENDFUNCTION, ByVal dwFlags As Integer) As Bool : End Function
        <DllImport("user32.dll")>
        Public Shared Function GetDC(ByVal handle As IntPtr) As IntPtr : End Function
        <DllImport("user32.dll", ExactSpelling:=True)>
        Public Shared Function ReleaseDC(ByVal handle As IntPtr, ByVal hDC As IntPtr) As Integer : End Function
        <DllImport("gdi32.dll")>
        Public Shared Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr : End Function
        <DllImport("gdi32.dll")>
        Public Shared Function DeleteDC(ByVal hdc As IntPtr) As Bool : End Function
        <DllImport("gdi32.dll")>
        Public Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr : End Function
        <DllImport("gdi32.dll")>
        Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Bool : End Function
    End Class
End Class

#End If