Module M_MSJ
#Region "Mensajes del Sistema"
    'Mensajes utilizados en el Sistema:
    Public Sub MsgBoxInfo(ByVal mensaje As String, ByVal titulo As String)
        'Mensaje de información
        MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Sub MsgBoxError(ByVal mensaje As String, ByVal titulo As String)
        'Mensaje de Error
        MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MsgBoxYesNo(ByVal mensaje As String, ByVal titulo As String)
        'Mensaje de Pregunta
        MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Sub
#End Region
End Module
