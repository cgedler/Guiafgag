Module M_VALD
#Region "Variables Publicas del Sistema"
    Public KeyAscii As Short
#End Region
#Region "Validación de los TextBox"
    Function NUMEROS(ByVal KeyAscii As Integer) As Integer
        If InStr("1234567890", Chr(KeyAscii)) = 0 Then
            NUMEROS = 0
        Else
            NUMEROS = KeyAscii
        End If
        ' teclas adicionales permitidas
        If KeyAscii = 8 Then NUMEROS = KeyAscii ' Backspace
        If KeyAscii = 13 Then NUMEROS = KeyAscii ' Enter
    End Function
    Function NUMEROSDEC(ByVal KeyAscii As Integer) As Integer
        If InStr("1234567890,", Chr(KeyAscii)) = 0 Then
            NUMEROSDEC = 0
        Else
            NUMEROSDEC = KeyAscii
        End If
        ' teclas adicionales permitidas
        If KeyAscii = 8 Then NUMEROSDEC = KeyAscii ' Backspace
        If KeyAscii = 13 Then NUMEROSDEC = KeyAscii ' Enter
    End Function
    Function NUMEROSDEC2(ByVal KeyAscii As Integer) As Integer
        If InStr("1234567890.", Chr(KeyAscii)) = 0 Then
            NUMEROSDEC2 = 0
        Else
            NUMEROSDEC2 = KeyAscii
        End If
        ' teclas adicionales permitidas
        If KeyAscii = 8 Then NUMEROSDEC2 = KeyAscii ' Backspace
        If KeyAscii = 13 Then NUMEROSDEC2 = KeyAscii ' Enter
    End Function
    Function VALIDA_telefono(ByVal KeyAscii As Integer) As Integer
        If InStr("1234567890.-()", Chr(KeyAscii)) = 0 Then
            VALIDA_telefono = 0
        Else
            VALIDA_telefono = KeyAscii
        End If
        'teclas adicionales permitidas
        If KeyAscii = 8 Then VALIDA_telefono = KeyAscii ' Backspace
        If KeyAscii = 13 Then VALIDA_telefono = KeyAscii ' Enter
        If KeyAscii = 32 Then VALIDA_telefono = KeyAscii ' espacio
    End Function
    Function VALIDA_RIF(ByVal KeyAscii As Integer) As Integer
        If InStr("VJGE1234567890-", Chr(KeyAscii)) = 0 Then
            VALIDA_RIF = 0
        Else
            VALIDA_RIF = KeyAscii
        End If
        ' teclas adicionales permitidas
        If KeyAscii = 8 Then VALIDA_RIF = KeyAscii ' Backspace
        If KeyAscii = 13 Then VALIDA_RIF = KeyAscii ' Enter
    End Function
#End Region
End Module
