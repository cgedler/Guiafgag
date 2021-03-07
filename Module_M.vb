Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Module Module_M
    'Inicializar objusuario
    Public obj As New Guia_C()
    Public path As String = System.AppDomain.CurrentDomain.BaseDirectory()
#Region "Reportes en el Sistema"
    Public InformeNombre As String
    Public ReportTipo As String
    Public ReportTipoNum As Integer
    Public ReportPar As Integer
    Public Params As New ParameterValues
    Public Parametro As New ParameterDiscreteValue 'Valor a buscar en la base de datos
    Public ReportNombreComp As New ParameterDiscreteValue 'Nombre de la Empresa
    Public ReportRifComp As New ParameterDiscreteValue 'RIF de la Empresa
    Public ReportDirComp As New ParameterDiscreteValue 'Dirección de la Empresa
    Public ReportTelefono1Comp As New ParameterDiscreteValue 'Telefono 1
    Public ReportTelefono2Comp As New ParameterDiscreteValue 'Telefono 2
    Public ReportFaxComp As New ParameterDiscreteValue 'Fax
    Public ReportEmailComp As New ParameterDiscreteValue 'Email
    Public ReportWebComp As New ParameterDiscreteValue 'Web
    Public ReportMontoTexto As New ParameterDiscreteValue 'Monto a Texto
#End Region
#Region "DataGridViem"
    Public Sub Select_DGV_Guia()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select NumItem,co_art,art_des,cantidad,peso,iva,fpo,subtotal from GUIATEMP where [NumGuia]='" & obj._OBNumGuiaAle & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        My.Forms.Guia.DataGridView.DataSource = ds.Tables(0)
        ' Tiulo de cabeceras
        My.Forms.Guia.DataGridView.Columns(0).HeaderText = "Item"
        My.Forms.Guia.DataGridView.Columns(1).HeaderText = "Codigo"
        My.Forms.Guia.DataGridView.Columns(2).HeaderText = "Descripción"
        My.Forms.Guia.DataGridView.Columns(3).HeaderText = "Cantidad"
        My.Forms.Guia.DataGridView.Columns(4).HeaderText = "Peso"
        My.Forms.Guia.DataGridView.Columns(5).HeaderText = "IVA"
        My.Forms.Guia.DataGridView.Columns(6).HeaderText = "FPO"
        My.Forms.Guia.DataGridView.Columns(7).HeaderText = "SubTotal"
        ' Alineación de las cabeceras
        My.Forms.Guia.DataGridView.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
        ' Alineación de las celdas de cada columna
        My.Forms.Guia.DataGridView.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        My.Forms.Guia.DataGridView.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        My.Forms.Guia.DataGridView.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        My.Forms.Guia.DataGridView.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        My.Forms.Guia.DataGridView.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        ' Formato de las columnas
        My.Forms.Guia.DataGridView.Columns(4).DefaultCellStyle.Format = "#,#.00 Kgs"
        My.Forms.Guia.DataGridView.Columns(5).DefaultCellStyle.Format = "#,#.00 Bs"
        My.Forms.Guia.DataGridView.Columns(6).DefaultCellStyle.Format = "#,#.00 Bs"
        My.Forms.Guia.DataGridView.Columns(7).DefaultCellStyle.Format = "#,#.00 Bs"
        cnn2.Close()
    End Sub
    Public Function Sumar_DataGridView( _
     ByVal nombre_Columna As String, _
     ByVal Dgv As DataGridView) As Double
        Dim total As Double = 0
        Dim i As Integer = 0
        Try
            If Dgv.RowCount > 0 Then
                If Dgv.Rows(0).Cells(nombre_Columna).Value Is DBNull.Value Then
                    total = 0
                    Return total
                End If
                Try
                    For i = 0 To Dgv.RowCount - 1
                        total = total + CDbl(Dgv.Item(nombre_Columna.ToLower, i).Value)
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                End Try
            ElseIf Dgv.RowCount = 0 Then
                total = 0
                Return total
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Return total
    End Function
#End Region
End Module
