Imports System.Runtime.InteropServices
Imports System.Text
Module M_IMP
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function OpenFpctrl(ByVal PortName As String) As Boolean
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function CloseFpctrl() As Boolean
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function CheckFprinter() As Integer
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function ReadFpStatus(ByRef status As Integer, ByRef [error] As Integer) As Integer
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function SendCmd(ByRef status As Integer, ByRef [error] As Integer, ByVal cmd As String) As Integer
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function SendNCmd(ByRef status As Integer, ByRef [error] As Integer, ByVal buffer As String) As Integer
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function SendFileCmd(ByRef status As Integer, ByRef [error] As Integer, ByVal file As String) As Integer
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function UploadReportCmd(ByRef status As Integer, ByRef [error] As Integer, ByVal cmd As String, ByVal file As String) As Integer
    End Function
    <DllImport("TFHKAIF.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)> _
    Public Function UploadStatusCmd(ByRef status As Integer, ByRef [error] As Integer, ByVal cmd As String, ByVal respuesta As String) As Integer
    End Function

End Module
