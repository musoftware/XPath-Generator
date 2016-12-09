Option Strict On

Imports System.Runtime.InteropServices

Public Class WindowsClass
    Const WS_BORDER As Integer = 8388608
    Const WS_DLGFRAME As Integer = 4194304
    Const WS_CAPTION As Integer = WS_BORDER Or WS_DLGFRAME
    Const WS_SYSMENU As Integer = 524288
    Const WS_THICKFRAME As Integer = 262144
    Const WS_MINIMIZE As Integer = 536870912
    Const WS_MINIMIZEBOX As Integer = &H20000L

    Const WS_MAXIMIZEBOX As Integer = 65536
    Const GWL_STYLE As Integer = -16&
    Const GWL_EXSTYLE As Integer = -20&
    Const WS_EX_DLGMODALFRAME As Integer = &H1L
    Const SWP_NOMOVE As Integer = &H2
    Const SWP_NOSIZE As Integer = &H1
    Const SWP_FRAMECHANGED As Integer = &H20
    Const MF_BYPOSITION As UInteger = &H400
    Const MF_REMOVE As UInteger = &H1000
    Const WS_CLIPCHILDREN As Integer = &H2000000L
    <StructLayout(LayoutKind.Sequential)> Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Declare Auto Function GetWindowLong Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    Declare Auto Function SetWindowLong Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    Declare Auto Function SetWindowPos Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As Integer) As Boolean
    Public Declare Function GetWindowRect Lib "user32" (ByVal HWND As Integer, ByVal lpRect As RECT) As Integer
    Private Declare Function ShowWindow Lib "user32" (ByVal handle As IntPtr, ByVal nCmdShow As Integer) As Integer
    <DllImport("user32.dll", SetLastError:=True)> _
    Friend Shared Function MoveWindow(hWnd As IntPtr, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, bRepaint As Boolean) As Boolean
    End Function


    Public Shared Sub MakeExternalWindowBorderless(ByVal MainWindowHandle As IntPtr, width As Integer, height As Integer)
        Dim R As RECT
        GetWindowRect(MainWindowHandle.ToInt32(), R) 'I get the error just there!!
        ShowWindow(MainWindowHandle, 1)

        MoveWindow(MainWindowHandle, 0, -83, width, height + 85, True)
    End Sub
End Class