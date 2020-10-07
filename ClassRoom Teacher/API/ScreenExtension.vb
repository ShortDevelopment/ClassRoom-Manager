Imports System.ComponentModel
Imports System.Management
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Module ScreenExtension

    Private Enum MonitorOptions As UInteger
        MONITOR_DEFAULTTONULL = &H0
        MONITOR_DEFAULTTOPRIMARY = &H1
        MONITOR_DEFAULTTONEAREST = &H2
    End Enum
    <DllImport("user32.dll")>
    Private Function MonitorFromWindow(ByVal handle As IntPtr, ByVal flags As Int32) As IntPtr : End Function
    <DllImport("user32.dll")>
    Private Function GetMonitorInfo(ByVal hMonitor As IntPtr, ByVal lpmi As NativeMonitorInfo) As Boolean : End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Private Function MonitorFromPoint(pt As Point, dwFlags As MonitorOptions) As IntPtr
    End Function

    <Serializable, StructLayout(LayoutKind.Sequential)>
    Private Structure NativeRectangle
        Public Left As Int32
        Public Top As Int32
        Public Right As Int32
        Public Bottom As Int32

        Public Sub New(ByVal left As Int32, ByVal top As Int32, ByVal right As Int32, ByVal bottom As Int32)
            Me.Left = left
            Me.Top = top
            Me.Right = right
            Me.Bottom = bottom
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Private NotInheritable Class NativeMonitorInfo
        Public Size As Int32 = Marshal.SizeOf(GetType(NativeMonitorInfo))
        Public Monitor As NativeRectangle
        Public Work As NativeRectangle
        Public Flags As Int32
    End Class

    <DllImport("gdi32.dll")>
    Private Function GetDeviceCaps(ByVal hDC As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    Private Enum DeviceCap As Integer

        ''' <summary>
        ''' Device driver version
        ''' </summary>
        DRIVERVERSION = 0
        ''' <summary>
        ''' Device classification
        ''' </summary>
        TECHNOLOGY = 2
        ''' <summary>
        ''' Horizontal size in millimeters
        ''' </summary>
        HORZSIZE = 4
        ''' <summary>
        ''' Vertical size in millimeters
        ''' </summary>
        VERTSIZE = 6
        ''' <summary>
        ''' Horizontal width in pixels
        ''' </summary>
        HORZRES = 8
        ''' <summary>
        ''' Vertical height in pixels
        ''' </summary>
        VERTRES = 10
        ''' <summary>
        ''' Number of bits per pixel
        ''' </summary>
        BITSPIXEL = 12
        ''' <summary>
        ''' Number of planes
        ''' </summary>
        PLANES = 14
        ''' <summary>
        ''' Number of brushes the device has
        ''' </summary>
        NUMBRUSHES = 16
        ''' <summary>
        ''' Number of pens the device has
        ''' </summary>
        NUMPENS = 18
        ''' <summary>
        ''' Number of markers the device has
        ''' </summary>
        NUMMARKERS = 20
        ''' <summary>
        ''' Number of fonts the device has
        ''' </summary>
        NUMFONTS = 22
        ''' <summary>
        ''' Number of colors the device supports
        ''' </summary>
        NUMCOLORS = 24
        ''' <summary>
        ''' Size required for device descriptor
        ''' </summary>
        PDEVICESIZE = 26
        ''' <summary>
        ''' Curve capabilities
        ''' </summary>
        CURVECAPS = 28
        ''' <summary>
        ''' Line capabilities
        ''' </summary>
        LINECAPS = 30
        ''' <summary>
        ''' Polygonal capabilities
        ''' </summary>
        POLYGONALCAPS = 32
        ''' <summary>
        ''' Text capabilities
        ''' </summary>
        TEXTCAPS = 34
        ''' <summary>
        ''' Clipping capabilities
        ''' </summary>
        CLIPCAPS = 36
        ''' <summary>
        ''' Bitblt capabilities
        ''' </summary>
        RASTERCAPS = 38
        ''' <summary>
        ''' Length of the X leg
        ''' </summary>
        ASPECTX = 40
        ''' <summary>
        ''' Length of the Y leg
        ''' </summary>
        ASPECTY = 42
        ''' <summary>
        ''' Length of the hypotenuse
        ''' </summary>
        ASPECTXY = 44
        ''' <summary>
        ''' Shading and Blending caps
        ''' </summary>
        SHADEBLENDCAPS = 45

        ''' <summary>
        ''' Logical pixels inch in X
        ''' </summary>
        LOGPIXELSX = 88
        ''' <summary>
        ''' Logical pixels inch in Y
        ''' </summary>
        LOGPIXELSY = 90

        ''' <summary>
        ''' Number of entries in physical palette
        ''' </summary>
        SIZEPALETTE = 104
        ''' <summary>
        ''' Number of reserved entries in palette
        ''' </summary>
        NUMRESERVED = 106
        ''' <summary>
        ''' Actual color resolution
        ''' </summary>
        COLORRES = 108

        ' Printing related DeviceCaps. These replace the appropriate Escapes

        ''' <summary>
        ''' Physical Width in device units
        ''' </summary>
        PHYSICALWIDTH = 110
        ''' <summary>
        ''' Physical Height in device units
        ''' </summary>
        PHYSICALHEIGHT = 111
        ''' <summary>
        ''' Physical Printable Area x margin
        ''' </summary>
        PHYSICALOFFSETX = 112
        ''' <summary>
        ''' Physical Printable Area y margin
        ''' </summary>
        PHYSICALOFFSETY = 113
        ''' <summary>
        ''' Scaling factor x
        ''' </summary>
        SCALINGFACTORX = 114
        ''' <summary>
        ''' Scaling factor y
        ''' </summary>
        SCALINGFACTORY = 115

        ''' <summary>
        ''' Current vertical refresh rate of the display device (for displays only) in Hz
        ''' </summary>
        VREFRESH = 116
        ''' <summary>
        ''' Vertical height of entire desktop in pixels
        ''' </summary>
        DESKTOPVERTRES = 117
        ''' <summary>
        ''' Horizontal width of entire desktop in pixels
        ''' </summary>
        DESKTOPHORZRES = 118
        ''' <summary>
        ''' Preferred blt alignment
        ''' </summary>
        BLTALIGNMENT = 119

    End Enum

    <DllImport("User32.dll")>
    Private Function GetWindowDC(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Public Enum MONITOR_DPI_TYPE
        MDT_EFFECTIVE_DPI = 0
        MDT_ANGULAR_DPI = 1
        MDT_RAW_DPI = 2
        MDT_DEFAULT = MDT_EFFECTIVE_DPI
    End Enum


    <DllImport("shcore.dll")>
    Private Function GetDpiForMonitor(ByVal hMonitor As IntPtr, ByVal type As MONITOR_DPI_TYPE, <Out> ByRef dpiX As Integer, <Out> ByRef dpiY As Integer) As UInteger : End Function

    <DllImport("Shcore.dll", SetLastError:=True)>
    Private Sub GetScaleFactorForMonitor(ByVal hMon As IntPtr, <Out> ByRef factor As Integer) : End Sub

    Enum DEVICE_SCALE_FACTOR
        DEVICE_SCALE_FACTOR_INVALID
        SCALE_100_PERCENT
        SCALE_120_PERCENT
        SCALE_125_PERCENT
        SCALE_140_PERCENT
        SCALE_150_PERCENT
        SCALE_160_PERCENT
        SCALE_175_PERCENT
        SCALE_180_PERCENT
        SCALE_200_PERCENT
        SCALE_225_PERCENT
        SCALE_250_PERCENT
        SCALE_300_PERCENT
        SCALE_350_PERCENT
        SCALE_400_PERCENT
        SCALE_450_PERCENT
        SCALE_500_PERCENT
    End Enum

    <Extension>
    Public Function GetCorrectSize(ByRef screen As Screen) As Size

        Dim desktop = GetWindowDC(IntPtr.Zero)

        Dim LogicalScreenHeight As Integer = GetDeviceCaps(desktop, CInt(DeviceCap.VERTRES))
        Dim PhysicalScreenHeight As Integer = GetDeviceCaps(desktop, CInt(DeviceCap.DESKTOPVERTRES))
        Dim logpixelsy As Integer = GetDeviceCaps(desktop, CInt(DeviceCap.LOGPIXELSY))
        Dim screenScalingFactor As Single = CSng(PhysicalScreenHeight) / CSng(LogicalScreenHeight)
        Dim dpiScalingFactor As Single = CSng(logpixelsy) / CSng(96)

        ReleaseDC(IntPtr.Zero, desktop)


        Dim monitor = MonitorFromPoint(screen.Bounds.Location, MonitorOptions.MONITOR_DEFAULTTONEAREST)
        Dim dpix As Integer
        Dim dpiy As Integer
        GetDpiForMonitor(monitor, MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, dpix, dpiy)

        Dim ScaleFactor = dpix

        If Not monitor = IntPtr.Zero Then
            Dim monitorInfo = New NativeMonitorInfo()
            GetMonitorInfo(monitor, monitorInfo)
            Dim width = (monitorInfo.Monitor.Right - monitorInfo.Monitor.Left)
            Dim height = (monitorInfo.Monitor.Bottom - monitorInfo.Monitor.Top)
            Return New Size(width * screenScalingFactor, height * screenScalingFactor)
        End If
    End Function

    <Extension>
    Public Function GetScreenshot(ByRef screen As Screen)
        Dim bounds = New Rectangle(screen.Bounds.Location, screen.GetCorrectSize())
        Dim bmp As New Bitmap(bounds.Width, bounds.Height)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.CopyFromScreen(bounds.Location, New Point(0, 0), bounds.Size, CopyPixelOperation.SourceCopy)
        End Using
        Return bmp
    End Function

End Module
